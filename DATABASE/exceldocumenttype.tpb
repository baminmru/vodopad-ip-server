CREATE OR REPLACE TYPE BODY ExcelDocumentType AS


CONSTRUCTOR FUNCTION ExcelDocumentType RETURN SELF AS RESULT
IS
BEGIN

     -- Initialize "Constants"
     SELF.NL_CHAR :='
';

     SELF.CW_MULT := 5.5;

     -- Set Object Id
     IF SELF.object_id IS NULL THEN
        EXECUTE IMMEDIATE 'SELECT excelobjectid_seq.nextval FROM dual' INTO SELF.object_id;
     END IF;

     -- Initialize Rowcount variables

     row_count       := 0;
     temp_col_count  := 0;
     col_count       := 0;
     document_length := 0;

     BEGIN
        EXECUTE IMMEDIATE 'DELETE from ExcelDocumentStore WHERE object_id=:object_id' USING SELF.object_id;
     EXCEPTION
        WHEN NO_DATA_FOUND THEN
             NULL;
     END;

     -- Initialize index variables

     SELF.styleSegIndex         := 0;


     SELF.colDefSegIndex              := 0;
     SELF.dataSegIndex                := 0;
     SELF.worksheetSegIndex           := 0;
     SELF.rowsSegIndex                := 0;
     SELF.cellsSegIndex               := 0;
     SELF.sheetHeaderFooterSegIndex   := 0;
     SELF.sheetHeaderDataIndex        := 0;
     SELF.sheetFooterDataIndex        := 0;
     SELF.documentIndex               := 0;

     -- Worksheet Header Format Types
   WHF_FORMAT_PAGE     := '&amp;P';
   WHF_FORMAT_PAGES    := '&amp;N';
   WHF_FORMAT_DATE     := '&amp;D';
   WHF_FORMAT_TIME     := '&amp;T';
   WHF_FORMAT_FILEPATH := '&amp;Z';
   WHF_FORMAT_FILE     := '&amp;F';
   WHF_FORMAT_TAB      := '&amp;A';

   WHF_FORMAT_LEFT     := '&amp;L';
   WHF_FORMAT_CENTER   := '&amp;C';
   WHF_FORMAT_RIGHT    := '&amp;R';

   WHF_FORMAT_FONT     := '&amp;<F>';

    RETURN;

END;

/*======================================================================================================================*/
MEMBER PROCEDURE pushValue(p_index   NUMBER,
                           p_segment VARCHAR2,
                           p_value   VARCHAR2)
IS

   v_value VARCHAR2(4000) := NULL;

BEGIN

   v_value := LTRIM(RTRIM(p_value, ' '|| NL_CHAR), ' '|| NL_CHAR) ||NL_CHAR ;

   EXECUTE IMMEDIATE 'INSERT INTO ExcelDocumentStore(object_id,seg_index,segment,value,seg_length) VALUES (:object_id,:seg_index,:segment,:value,:seg_length)'
   USING SELF.object_id,p_index,p_segment,p_value,lengthc(v_value)+2;

END;

/*======================================================================================================================*/
MEMBER PROCEDURE purgeSegment(p_segment VARCHAR2 := NULL)
IS
BEGIN

  EXECUTE IMMEDIATE 'DELETE FROM ExcelDocumentStore WHERE object_id=:object_id AND segment=:segment'
  USING SELF.object_id,p_segment;

END;

/*======================================================================================================================*/
MEMBER PROCEDURE documentOpen
IS

  v_header VARCHAR2(4000) := NULL;

BEGIN

     row_count := 0;
     col_count := 0;
     temp_col_count := 0;
     document_length := 0;

      -- Create Header

      v_header := '<?xml version="1.0"?> '||NL_CHAR||
                  '<?mso-application progid="Excel.Sheet"?>'||NL_CHAR||
                  '<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"'||NL_CHAR||
                  ' xmlns:o="urn:schemas-microsoft-com:office:office"'||NL_CHAR||
                  ' xmlns:x="urn:schemas-microsoft-com:office:excel"'||NL_CHAR||
                  ' xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"'||NL_CHAR||
                  ' xmlns:html="http://www.w3.org/TR/REC-html40">'||NL_CHAR||
                  ' <DocumentProperties xmlns="urn:schemas-microsoft-com:office:office">'||NL_CHAR||
                  '  <LastAuthor>'||USER||'</LastAuthor>'||NL_CHAR||
                  '  <Created>'||TO_CHAR(SYSDATE,'YYYY-MM-DD')||'T'||TO_CHAR(SYSDATE,'HH:MI:SS')||'</Created>'||NL_CHAR||
                  '  <Version>11.6408</Version>'||NL_CHAR||
                  ' </DocumentProperties>'||NL_CHAR||
                  ' <ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">'||NL_CHAR||
                  '  <WindowHeight>8445</WindowHeight>'||NL_CHAR||
                  '  <WindowWidth>11115</WindowWidth>'||NL_CHAR||
                  '  <WindowTopX>720</WindowTopX>'||NL_CHAR||
                  '  <WindowTopY>375</WindowTopY>'||NL_CHAR||
                  '  <RefModeR1C1/>'||NL_CHAR||
                  '  <ProtectStructure>False</ProtectStructure>'||NL_CHAR||
                  '  <ProtectWindows>False</ProtectWindows>'||NL_CHAR||
                  ' </ExcelWorkbook>';

      -- Add header segment

      documentIndex := documentIndex + 1;
      pushValue(documentIndex,'DOCUMENT',v_header);


END;
/*======================================================================================================================*/

   /*------------------*/
   /*  Document Close  */
   /*------------------*/

   MEMBER PROCEDURE documentClose
   IS

      /*CURSOR crsrSegmentData(cv_object_id NUMBER,
                             cv_segment   VARCHAR2) IS
      SELECT
              value
      FROM
              ExcelDocumentStore
      WHERE
              object_id = cv_object_id
      AND     segment = cv_segment
      ORDER BY seg_index ASC;*/


   BEGIN


      documentIndex := documentIndex + 1;
      pushValue(documentIndex,'DOCUMENT','</Workbook>');

      EXECUTE IMMEDIATE 'SELECT SUM(seg_length) FROM ExcelDocumentStore WHERE object_id=:object_id AND segment = ''DOCUMENT'''
      INTO document_length USING SELF.object_id;

   END;

/*======================================================================================================================*/

MEMBER PROCEDURE worksheetOpen(p_worksheetname VARCHAR2 := NULL)
IS

    v_worksheet_open VARCHAR2(2000) := NULL;

BEGIN

     v_worksheet_open := ' <Worksheet ss:Name="'||p_worksheetname||'"> '||NL_CHAR||
                         '   <Table ss:ExpandedColumnCount="<colcnt>" ss:ExpandedRowCount="<rowcnt>" x:FullColumns="1" x:FullRows="1">';


     worksheetSegIndex := worksheetSegIndex + 1;
     pushValue(worksheetSegIndex,'WORKSHEET',v_worksheet_open);

END;

/*======================================================================================================================*/

MEMBER PROCEDURE  worksheetHeaderFooterOpen
IS

  v_worksheetheader VARCHAR2(2000) := NULL;

BEGIN

  v_worksheetheader := '<WorksheetOptions xmlns="urn:schemas-microsoft-com:office:excel">';

  sheetHeaderFooterSegIndex := sheetHeaderFooterSegIndex + 1;
  pushValue(sheetHeaderFooterSegIndex,'SHEETHEADERFOOTER',v_worksheetheader);

END;

/*======================================================================================================================*/

MEMBER PROCEDURE worksheetHeaderValues(p_headerstring VARCHAR2,
                                       p_fontsize     VARCHAR2)
IS

   v_headerstring VARCHAR2(1000) := p_headerstring;

BEGIN




  IF p_fontsize IS NOT NULL THEN

     v_headerstring := REPLACE(v_headerstring,'<F>',p_fontsize);

  ELSE

     v_headerstring := REPLACE(v_headerstring,'&amp;<F>',NULL);

  END IF;


  sheetHeaderDataIndex := sheetHeaderDataIndex + 1;
  pushValue(sheetHeaderDataIndex,'SHEETHEADERDATA',v_headerstring);

END;

/*======================================================================================================================*/

MEMBER PROCEDURE worksheetFooterValues(p_footerstring VARCHAR2,
                                       p_fontsize     VARCHAR2)
IS

   v_footerstring VARCHAR2(1000) := p_footerstring;

BEGIN




  IF p_fontsize IS NOT NULL THEN

     v_footerstring := REPLACE(v_footerstring,'<F>',p_fontsize);

  ELSE

     v_footerstring := REPLACE(v_footerstring,'&amp;<F>',NULL);

  END IF;


  sheetFooterDataIndex := sheetFooterDataIndex + 1;
  pushValue(sheetFooterDataIndex,'SHEETFOOTERDATA',v_footerstring);

END;

/*======================================================================================================================*/
MEMBER PROCEDURE worksheetHeaderFooterClose
IS

  CURSOR crsrSegmentData(cv_object_id NUMBER,
                         cv_segment   VARCHAR2) IS
  SELECT
         value
  FROM
         ExcelDocumentStore
  WHERE
         object_id = cv_object_id
  AND    segment   = cv_segment
  ORDER BY seg_index ASC;

  v_worksheetheaderfooter VARCHAR2(8000) := NULL;

  v_headerstring          VARCHAR2(4000) := NULL;
  v_footerstring          VARCHAR2(4000) := NULL;

BEGIN

  v_worksheetheaderfooter := '  <PageSetup> '||NL_CHAR||
                             '     <Header x:Data="<headerstring>"/> '||NL_CHAR||
                             '     <Footer x:Data="<footerstring>"/> '||NL_CHAR||
                             '  </PageSetup> '||NL_CHAR||
                             '</WorksheetOptions>';


  FOR sheetheader_rec IN crsrSegmentData(SELF.object_id,'SHEETHEADERDATA') LOOP

    v_headerstring := v_headerstring||sheetheader_rec.value;

  END LOOP;

  v_worksheetheaderfooter := REPLACE(v_worksheetheaderfooter,'<headerstring>',v_headerstring);

  purgeSegment('SHEETHEADERDATA');

  FOR sheetfooter_rec IN crsrSegmentData(SELF.object_id,'SHEETFOOTERDATA') LOOP

    v_footerstring := v_footerstring||sheetfooter_rec.value;

  END LOOP;

  v_worksheetheaderfooter := REPLACE(v_worksheetheaderfooter,'<footerstring>',v_footerstring);

  purgeSegment('SHEETFOOTERDATA');

  sheetHeaderFooterSegIndex := sheetHeaderFooterSegIndex + 1;
  pushValue(sheetHeaderFooterSegIndex,'SHEETHEADERFOOTER',v_worksheetheaderfooter);

END;

/*======================================================================================================================*/

   /*-------------------*/
   /*  Worksheet Close  */
   /*-------------------*/

MEMBER PROCEDURE worksheetClose
IS

  CURSOR crsrSegmentData(cv_object_id  NUMBER,
                         cv_segment    VARCHAR2) IS
  SELECT
         seg_index,
         value
  FROM
         ExcelDocumentStore
  WHERE
         object_id = cv_object_id
  AND    segment   = cv_segment
  ORDER BY seg_index ASC;

  v_seg_data VARCHAR2(32000) := NULL;

BEGIN

  -- Row and Col counts
  FOR worksheet_rec IN crsrSegmentData(object_id,'WORKSHEET') LOOP

     v_seg_data := worksheet_rec.value;

     v_seg_data := REPLACE(v_seg_data,'<rowcnt>',SELF.row_count);
     v_seg_data := REPLACE(v_seg_data,'<colcnt>',SELF.col_count);

     EXECUTE IMMEDIATE 'UPDATE ExcelDocumentStore SET value = :value WHERE object_id=:object_id AND seg_index = :seg_index AND segment=''WORKSHEET'''
     USING v_seg_data,object_id,worksheet_rec.seg_index;


  END LOOP;


  -- Add column defs
  FOR coldef_rec IN crsrSegmentData(SELF.object_id,'COLDEF') LOOP

    worksheetSegIndex := worksheetSegIndex + 1;
    pushValue(worksheetSegIndex,'WORKSHEET',coldef_rec.value);

  END LOOP;

  -- Add data seg
  FOR data_rec IN crsrSegmentData(SELF.object_id,'COLDATA') LOOP

    worksheetSegIndex := worksheetSegIndex + 1;
    pushValue(worksheetSegIndex,'WORKSHEET',data_rec.value);

  END LOOP;

  worksheetSegIndex := worksheetSegIndex + 1;
  pushValue(worksheetSegIndex,'WORKSHEET','</Table>');


  -- Set Worksheet header

  FOR sheetheaderfooter_rec IN crsrSegmentData(SELF.object_id,'SHEETHEADERFOOTER') LOOP

    worksheetSegIndex := worksheetSegIndex + 1;
    pushValue(worksheetSegIndex,'WORKSHEET',sheetheaderfooter_rec.value);

  END LOOP;

  -- Close Worksheet tagset

  worksheetSegIndex := worksheetSegIndex + 1;
  pushValue(worksheetSegIndex,'WORKSHEET','</Worksheet>');

  -- Add worksheet segment
  FOR worksheet_rec IN crsrSegmentData(SELF.object_id,'WORKSHEET') LOOP

      documentIndex := documentIndex + 1;
      pushValue(documentIndex,'DOCUMENT',worksheet_rec.value);

  END LOOP;


  purgeSegment('COLDEF');
  purgeSegment('COLDATA');
  purgeSegment('WORKSHEET');
  purgeSegment('ROW');
  purgeSegment('CELL');

  -- Reset Row and Column Counts
  SELF.row_count := 0;
  SELF.col_count := 0;
  SELF.temp_col_count := 0;

END;

/*======================================================================================================================*/

   /*-------------------*/
   /*  Styles Open      */
   /*-------------------*/

MEMBER PROCEDURE stylesOpen
IS
BEGIN

  styleSegIndex := styleSegIndex + 1;
  pushValue(styleSegIndex,'STYLE','<Styles>');

END;

/*======================================================================================================================*/

   /*-------------------*/
   /*  Styles Close     */
   /*-------------------*/

MEMBER PROCEDURE stylesClose
IS

      CURSOR crsrSegmentData( cv_object_id   NUMBER,
                              cv_segment     VARCHAR2) IS
      SELECT
              value
      FROM
              ExcelDocumentStore
      WHERE
              object_id = cv_object_id
      AND     segment = cv_segment
      ORDER BY seg_index ASC;

BEGIN

  styleSegIndex := styleSegIndex + 1;
  pushValue(styleSegIndex,'STYLE','</Styles>');

   -- Add style segment
   FOR style_rec IN crsrSegmentData(SELF.object_id,'STYLE') LOOP

      documentIndex := documentIndex + 1;
      pushValue(documentIndex,'DOCUMENT',style_rec.value);

   END LOOP;

   purgeSegment('STYLE');

END;

/*=====================================================================================================================*/

   /*-----------*/
   /* Row Open  */
   /*-----------*/

MEMBER PROCEDURE rowOpen(p_style       VARCHAR2 := NULL,
                         p_custom_attr VARCHAR2 := NULL)
IS

  v_row VARCHAR2(100) := '<Row ss:StyleID="<style>" <custom>>';

BEGIN

   SELF.temp_col_count := 0;
   SELF.row_count      := SELF.row_count + 1;

   IF p_style IS NOT NULL THEN

        v_row := REPLACE(v_row,'<style>',p_style);

   ELSE

        v_row := REPLACE(v_row,' ss:StyleID="<style>"',NULL);

   END IF;

   v_row := REPLACE(v_row,'<custom>',p_custom_attr);

   rowsSegIndex := rowsSegIndex + 1;
   pushValue(rowsSegIndex,'ROW',v_row);

END;

/*=====================================================================================================================*/

   /*-------------*/
   /*  Row Close  */
   /*-------------*/

MEMBER PROCEDURE rowClose
IS

  CURSOR crsrSegmentData(cv_object_id NUMBER,
                         cv_segment  VARCHAR2) IS
  SELECT
         seg_index,
         value
  FROM
         ExcelDocumentStore
  WHERE
         object_id = cv_object_id
  AND    segment   = cv_segment
  ORDER BY seg_index ASC;

BEGIN

  IF SELF.temp_col_count >= SELF.col_count THEN

    SELF.col_count := SELF.temp_col_count;

  END IF;


  FOR cell_rec IN crsrSegmentData(SELF.object_id,'CELL') LOOP

    rowsSegIndex := rowsSegIndex + 1;
    pushValue(rowsSegIndex,'ROW',cell_rec.value);

  END LOOP;

  rowsSegIndex := rowsSegIndex + 1;
  pushValue(rowsSegIndex,'ROW','</Row>');

  FOR row_rec IN crsrSegmentData(SELF.object_id,'ROW') LOOP

    dataSegIndex := dataSegIndex + 1;
    pushValue(dataSegIndex,'COLDATA',row_rec.value);

  END LOOP;


  purgeSegment('ROW');
  purgeSegment('CELL');

END;

/*=====================================================================================================================*/

  /*------------------*/
  /*  Default Style   */
  /*------------------*/

MEMBER PROCEDURE defaultStyle
IS

    v_default_style VARCHAR2(2000) := NULL;

BEGIN

  v_default_style :=   '<Style ss:ID="Default" ss:Name="Normal"> '||NL_CHAR||
                       '<Alignment ss:Vertical="Bottom"/> '||NL_CHAR||
                       '<Borders/>'||NL_CHAR||
                       '<Font/> '||NL_CHAR||
                       '<Interior/> '||NL_CHAR||
                       '<NumberFormat/> '||NL_CHAR||
                       '<Protection/> '||NL_CHAR||
                       '</Style> ';

  styleSegIndex := styleSegIndex + 1;
  pushValue(styleSegIndex,'STYLE',v_default_style);

END;

/*=====================================================================================================================*/

  /*-----------------------------------------------*/
  /* Create a style to apply to one or more cells  */
  /* Some style items are not currently supported: */
  /* --Borders, Fill patterns, etc                 */
  /* -- Style ID is required.                      */
  /*-----------------------------------------------*/

MEMBER PROCEDURE createStyle(p_style_id         VARCHAR2 := NULL,
                         p_font             VARCHAR2 := NULL,
                         p_ffamily          VARCHAR2 := NULL,
                         p_fsize            VARCHAR2 := NULL,
                         p_bold             VARCHAR2 := NULL,
                         p_italic           VARCHAR2 := NULL,
                         p_underline        VARCHAR2 := NULL,
                         p_text_color       VARCHAR2 := NULL,
                         p_cell_color       VARCHAR2 := NULL,
                         p_cell_pattern     VARCHAR2 := NULL,
                         p_align_vertical   VARCHAR2 := NULL,
                         p_align_horizontal VARCHAR2 := NULL,
                         p_wrap_text        VARCHAR2 := NULL,
                         p_number_format    VARCHAR2 := NULL,
                         p_custom_xml       VARCHAR2 := NULL)
IS

      v_style_tag VARCHAR2(5000) := '<Style ss:ID="p_style_id">'||NL_CHAR||
                                    '  <Alignment ss:Vertical="p_align_vertical" ss:Horizontal="p_align_horizontal" ss:WrapText="p_wrap_text"/>'||NL_CHAR||
                                    '  <Font ss:FontName="p_font" x:Family="p_ffamily" ss:Size="p_fsize" ss:Color="p_text_color" ss:Bold="p_bold" ss:Italic="p_italic" ss:Underline="p_underline"/>'||NL_CHAR||
                                    '  <Interior ss:Color="p_cell_color" ss:Pattern="p_cell_pattern"/>'||NL_CHAR||
                                    '  <NumberFormat ss:Format="p_number_format"/>'||NL_CHAR||
                                    '  <Custom> '||NL_CHAR||
                                    '</Style>';
BEGIN

        IF p_style_id IS NOT NULL THEN

           -- Style Label/Name
           v_style_tag := REPLACE(v_style_tag,'p_style_id',p_style_id);

          -- Font Family
          IF p_ffamily IS NOT NULL THEN
             v_style_tag := REPLACE(v_style_tag,'p_ffamily',p_ffamily);
           ELSE
             v_style_tag := REPLACE(v_style_tag,' x:Family="p_ffamily"',NULL);
           END IF;

          -- Font
          IF p_font IS NOT NULL THEN
             v_style_tag := REPLACE(v_style_tag,'p_font',p_font);
           ELSE
             v_style_tag := REPLACE(v_style_tag,' ss:FontName="p_font"',NULL);
           END IF;

          -- Font Size
          IF p_fsize IS NOT NULL THEN
            v_style_tag := REPLACE(v_style_tag,'p_fsize',p_fsize);
          ELSE
            v_style_tag := REPLACE(v_style_tag,' ss:Size="p_fsize"',NULL);
          END IF;

          -- Bold
          IF p_bold = 'Y' THEN
            v_style_tag := REPLACE(v_style_tag,'p_bold','1');
          ELSE
            v_style_tag := REPLACE(v_style_tag,' ss:Bold="p_bold"',NULL);
          END IF;

          -- Italics
          IF p_italic = 'Y' THEN
            v_style_tag := REPLACE(v_style_tag,'p_italic','1');
          ELSE
            v_style_tag := REPLACE(v_style_tag,' ss:Italic="p_italic"',NULL);
          END IF;

          -- Underline
          IF p_underline IS NOT NULL THEN
            v_style_tag := REPLACE(v_style_tag,'p_underline',p_underline);
          ELSE
            v_style_tag := REPLACE(v_style_tag,' ss:Underline="p_underline"',NULL);
          END IF;

          -- Text color
          IF p_text_color IS NOT NULL THEN
            v_style_tag := REPLACE(v_style_tag,'p_text_color',p_text_color);
          ELSE
            v_style_tag := REPLACE(v_style_tag,' ss:Color="p_text_color"',NULL);
          END IF;

          -- Cell Color
          IF p_cell_color IS NOT NULL THEN
            v_style_tag := REPLACE(v_style_tag,'p_cell_color',p_cell_color);
          ELSE
            v_style_tag := REPLACE(v_style_tag,' ss:Color="p_cell_color"',NULL);
          END IF;

          -- Cell Pattern
          IF p_cell_pattern IS NOT NULL THEN
            v_style_tag := REPLACE(v_style_tag,'p_cell_pattern',p_cell_pattern);
          ELSE
            v_style_tag := REPLACE(v_style_tag,' ss:Pattern="p_cell_pattern"',NULL);
          END IF;

          -- Text Vertical Alignment
          IF p_align_vertical IS NOT NULL THEN
             v_style_tag := REPLACE(v_style_tag,'p_align_vertical',p_align_vertical);
          ELSE
             v_style_tag := REPLACE(v_style_tag,' ss:Vertical="p_align_vertical"',NULL);
          END IF;

          -- Text Horizontal Alignment
          IF p_align_horizontal IS NOT NULL THEN
             v_style_tag := REPLACE(v_style_tag,'p_align_horizontal',p_align_horizontal);
          ELSE
             v_style_tag := REPLACE(v_style_tag,' ss:Horizontal="p_align_horizontal"',NULL);
          END IF;

          -- Text Wrap
          IF p_wrap_text = 'Y' THEN
             v_style_tag := REPLACE(v_style_tag,'p_wrap_text','1');
          ELSE
             v_style_tag := REPLACE(v_style_tag,' ss:WrapText="p_wrap_text"',NULL);
          END IF;

          -- Number Formatting
          IF p_number_format IS NOT NULL THEN
             v_style_tag := REPLACE(v_style_tag,'p_number_format',p_number_format);
          ELSE
             v_style_tag := REPLACE(v_style_tag,' ss:Format="p_number_format"',NULL);
          END IF;

        END IF;

        -- Custom XML
           IF p_custom_xml IS NOT NULL THEN
              v_style_tag := REPLACE(v_style_tag, '<Custom>',p_custom_xml);
           ELSE
              v_style_tag := REPLACE(v_style_tag,' <Custom> '||NL_CHAR,NULL);
           END IF;

     styleSegIndex := styleSegIndex + 1;
     pushValue(styleSegIndex,'STYLE',v_style_tag);


   END;

/*======================================================================================================================*/

MEMBER PROCEDURE defineColumn(p_index VARCHAR2  := NULL,
                              p_width NUMBER    := NULL,
                              p_custom_attr VARCHAR2 := NULL)
IS

   v_width  NUMBER(12)     := 0;
   v_coltag VARCHAR2(4000) := '<Column ss:Index="<index>" ss:AutoFitWidth="0" ss:Width="<width>" <custom> />';

BEGIN

      IF p_width IS NOT NULL THEN

        v_width := TRUNC(p_width*CW_MULT);
        v_coltag := REPLACE(v_coltag,'<width>',v_width);

      ELSE

        v_coltag := REPLACE(v_coltag,'ss:Width="<width>"',NULL);

      END IF;

      IF p_index IS NOT NULL THEN

        v_coltag := REPLACE(v_coltag,'<index>',p_index);

      ELSE

        v_coltag := REPLACE(v_coltag,'ss:Index="<index>"',NULL);

      END IF;

      v_coltag := REPLACE(v_coltag,'<custom>',p_custom_attr);

     colDefSegIndex := colDefSegIndex + 1;
     pushValue(colDefSegIndex,'COLDEF',v_coltag);


END;

/*=============================================================================================================================*/

MEMBER PROCEDURE addCell(p_col_index   VARCHAR2 := NULL,
                         p_data        VARCHAR2 := NULL,
                         p_data_type   VARCHAR2 := 'String',
                         p_style       VARCHAR2 := NULL,
                         p_formula     VARCHAR2 := NULL,
                         p_custom_attr VARCHAR2 := NULL)
IS

      v_cell VARCHAR2(32000) := '<Cell ss:Index="<index>" ss:Formula="=<formula>" ss:StyleID="<style>" <custom> ><Data ss:Type="<datatype>"><p_data></Data></Cell>';

BEGIN


       temp_col_count := temp_col_count + 1;


       -- CELL INDEX
       IF p_col_index IS NOT NULL THEN

          v_cell := REPLACE(v_cell,'<index>',p_col_index);

       ELSE

          v_cell := REPLACE(v_cell,' ss:Index="<index>"',NULL);

       END IF;

       -- CELL FORMULA
       IF p_formula IS NOT NULL THEN

          v_cell := REPLACE(v_cell,'<formula>',p_formula);

       ELSE

          v_cell := REPLACE(v_cell,' ss:Formula="=<formula>"',NULL);

       END IF;

       -- CELL DATA
       IF p_data IS NOT NULL THEN

          v_cell := REPLACE(v_cell,'<p_data>',p_data);

       ELSE

          v_cell := REPLACE(v_cell,'<p_data>',NULL);

       END IF;

       -- CELL DATA TYPE
       IF p_data_type IS NOT NULL THEN

          v_cell := REPLACE(v_cell,'<datatype>',p_data_type);

       ELSE

          v_cell := REPLACE(v_cell,' ss:Type="<datatype>"',NULL);

       END IF;

       --CELL STYLE
       IF p_style IS NOT NULL THEN

          v_cell := REPLACE(v_cell,'<style>',p_style);

       ELSE

          v_cell := REPLACE(v_cell,' ss:StyleID="<style>"',NULL);

       END IF;

       -- Custom Attribute
       v_cell := REPLACE(v_cell,'<custom>',p_custom_attr);

     cellsSegIndex := cellsSegIndex + 1;
     pushValue(cellsSegIndex,'CELL',v_cell);
END;

/*================================================================================================================*/
MEMBER PROCEDURE displayDocument
IS


  CURSOR crsrSegmentData(cv_object_id NUMBER,
                         cv_segment   VARCHAR2) IS
  SELECT
         seg_index,
         value
  FROM
         ExcelDocumentStore
  WHERE
         object_id = cv_object_id
  AND    segment   = cv_segment
  ORDER BY seg_index ASC;

BEGIN

     -- Prepare Headers
     owa_util.mime_header('application/vnd.ms-excel',FALSE);
     --htp.p('Content-Length: '||document_length||NL_CHAR);
     htp.p('Content-Length: '||document_length);
     owa_util.http_header_close;


     FOR doc_rec  IN crsrSegmentData(SELF.object_id,'DOCUMENT') LOOP
         htp.p(doc_rec.value);
    END LOOP;

END;

/*================================================================================================================*/

MEMBER FUNCTION getDocument RETURN CLOB
IS

  CURSOR crsrSegmentData(cv_object_id NUMBER,
                         cv_segment   VARCHAR2) IS
  SELECT
         seg_index,
         value,
         lengthc(value) seg_length
  FROM
         ExcelDocumentStore
  WHERE
         object_id = cv_object_id
  AND    segment   = cv_segment
  ORDER BY seg_index ASC;

 v_clob  CLOB;

BEGIN

      -- BuildDocument CLOB
      DBMS_LOB.createTemporary(v_clob,FALSE,DBMS_LOB.CALL);
      FOR doc_rec IN crsrSegmentData(SELF.object_id,'DOCUMENT') LOOP

         DBMS_LOB.writeappend(v_clob,doc_rec.seg_length,doc_rec.value);

      END LOOP;

    RETURN v_clob;

END;

/*=========================================================================================================================*/

MEMBER FUNCTION getDocumentData RETURN ExcelDocumentLine
IS

  CURSOR crsrDocumentData(cv_object_id NUMBER,
                          cv_segment   VARCHAR2) IS
  SELECT
         value
  FROM
         ExcelDocumentStore
  WHERE
         object_id = cv_object_id
  AND    segment   = cv_segment
  ORDER BY seg_index ASC;

  rec_docData   ExcelDocumentLine := ExcelDocumentLine();

BEGIN

   FOR doc_rec IN crsrDocumentData(SELF.object_id,'DOCUMENT') LOOP

      rec_docData.EXTEND;
      rec_docData(rec_docData.COUNT) := doc_rec.value;

   END LOOP;

   RETURN rec_docData;

END;

/*=========================================================================================================================*/

/*---------------*/
/* END TYPE BODY */
/*---------------*/
END;
/

