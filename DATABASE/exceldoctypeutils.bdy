CREATE OR REPLACE PACKAGE BODY ExcelDocTypeUtils AS


/*

   Function that returns the element at the requested position in a delimited string.

*/
FUNCTION getStringElement(p_string    VARCHAR2,
                          p_element   NUMBER,
                          p_delimiter VARCHAR2 := ',',
                          p_level     NUMBER   := 0)   RETURN VARCHAR2
IS

   v_string   VARCHAR2(2000) := NULL;
   v_element  VARCHAR2(2000) := NULL;
   v_next     VARCHAR2(2000) := NULL;

   v_level    NUMBER(4)      := 0;


BEGIN

   v_level := p_level + 1;

   v_element  := substr(p_string||p_delimiter,1,instr(p_string||p_delimiter,p_delimiter)-1);

   -- need to look ahead to make sure we handle the null elements.
   v_next     := substr(p_string||p_delimiter,instr(p_string||p_delimiter,p_delimiter),length(p_delimiter));

   IF ((v_level >= p_element) OR (v_element IS NULL AND v_next != p_delimiter)) THEN

      RETURN v_element;

   ELSE

      v_string := substr(p_string||p_delimiter,instr(p_string||p_delimiter,p_delimiter)+1,length(p_string));

      RETURN getStringElement(v_string,p_element,p_delimiter,v_level);

   END IF;


END;

/*=========================================================================================================*/
/*
    This function executes the given query and returns the data in a RESULT_TABLE Collection object.

*/

FUNCTION buildDataSet(p_query_string VARCHAR2 := NULL,
                      p_col_count    NUMBER   := 0) RETURN RESULT_TABLE
IS


  v_row_symbol       VARCHAR2(20)   := 'v_row';
  v_row_fetch        VARCHAR2(1000) := NULL;
  v_row_extend       NUMBER(3)      := p_col_count;

  v_query            VARCHAR2(16000) := p_query_string;

  v_result_proc      VARCHAR2(32000) := 'DECLARE'||chr(10)||
                                        ' TYPE t_refcursor IS REF CURSOR; '||chr(10)||
                                        ' v_row T_ROW := T_ROW(); '||chr(10)||
                                        ' v_query VARCHAR2(4000) := ''<q>''; '||chr(10)||
                                        ' v_refcur t_refcursor; '||chr(10)||
                                       'BEGIN '||chr(10)||
                                       '   OPEN v_refcur FOR v_query; '||chr(10)||
                                       '   LOOP '||chr(10)||
                                       '    v_row.extend(<e>); '||chr(10)||
                                       '    FETCH v_refcur INTO <f> ;'||chr(10)||
                                       '    EXIT WHEN v_refcur%NOTFOUND; '||chr(10)||
                                       '    ExcelDocTypeUtils.pv_result_table.EXTEND; '||chr(10)||
                                       '    ExcelDocTypeUtils.pv_result_table(ExcelDocTypeUtils.pv_result_table.COUNT) := v_row; '||chr(10)||
                                       '    v_row.DELETE; '||chr(10)||
                                       '   END LOOP; '||chr(10)||
                                       'END; ';

BEGIN

   FOR x IN 1 .. v_row_extend LOOP

      v_row_fetch := v_row_fetch||v_row_symbol||'('||x||'),';

   END LOOP;

   v_row_fetch := RTRIM(v_row_fetch,',');

   v_result_proc := REPLACE(v_result_proc,'<q>',REPLACE(v_query,'''',''''''));

   v_result_proc := REPLACE(v_result_proc,'<e>',to_char(v_row_extend));

   v_result_proc := REPLACE(v_result_proc,'<f>',v_row_fetch);


   pv_result_table := RESULT_TABLE();

   EXECUTE IMMEDIATE v_result_proc;

   RETURN pv_result_table;

END;


/*======================================================================================================================================================================*/
/*

   This functiom constructs and returns an ExcelDocumentType based upon the parameters passed
   in by the WORKSHEET_TABLE type parameter.


*/
FUNCTION createExcelDocument(p_worksheet_data WORKSHEET_TABLE,
                             p_style_data     STYLE_LIST := STYLE_LIST()) RETURN ExcelDocumentType
IS

  resultDocument        ExcelDocumentType;
  v_row                 T_ROW := T_ROW();
  v_results             RESULT_TABLE := RESULT_TABLE();

  v_title               T_SHEET_TITLE := NULL;

  v_style               T_STYLE_DEF := NULL;

  v_default_col_width   NUMBER(3)    := 30;
  v_col_width           NUMBER(3)    := 0;

  v_default_data_type   VARCHAR2(6)  := 'String';
  v_data_type           VARCHAR2(20) := NULL;
  v_data_style          VARCHAR2(50) := NULL;

  v_style_list          VARCHAR2(4000) := ';';

  v_count_rows          NUMBER(10);
  v_formula             VARCHAR2(100);

BEGIN

  BEGIN

     COMMIT;

  EXCEPTION
     WHEN OTHERS THEN NULL;
  END;



  resultDocument := ExcelDocumentType();

  -- Open Document
  resultDocument.documentOpen;

  -- Define Customs Styles
  resultDocument.stylesOpen;

  resultDocument.defaultStyle;

  /* Style for Column Header Row */
  resultDocument.createStyle(p_style_id =>'ColumnHeader',
                                    p_font     =>'Times New Roman',
                                    p_ffamily  =>'Roman',
                                    p_fsize    =>'10',
                                    p_bold     =>'Y',
                                    p_underline =>'Single',
                                    p_align_horizontal=>'Center',
                                    p_align_vertical=>'Bottom');


  FOR x IN 1 .. p_style_data.COUNT LOOP

    v_style := p_style_data(x);

    v_style_list := v_style_list||';'||UPPER(v_style.p_style_id);

    resultDocument.createStyle(p_style_id         => UPPER(v_style.p_style_id),
                               p_font             => v_style.p_font,
                               p_ffamily          => v_style.p_ffamily,
                               p_fsize            => v_style.p_fsize,
                               p_bold             => v_style.p_bold,
                               p_italic           => v_style.p_italic,
                               p_underline        => v_style.p_underline,
                               p_text_color       => v_style.p_text_color,
                               p_cell_color       => v_style.p_cell_color,
                               p_cell_pattern     => v_style.p_cell_pattern,
                               p_align_vertical   => v_style.p_align_vertical,
                               p_align_horizontal => v_style.p_align_horizontal,
                               p_wrap_text        => v_style.p_wrap_text,
                               p_number_format    => v_style.p_number_format,
                               p_custom_xml       => v_style.p_custom_xml);


  END LOOP;

  resultDocument.stylesClose;


  FOR ws_index IN 1 .. p_worksheet_data.COUNT LOOP

     -- Open Worksheets

     resultDocument.worksheetOpen(p_worksheet_data(ws_index).worksheet_name);


     -- Define Columns

     FOR colnum IN 1 .. p_worksheet_data(ws_index).col_count LOOP

       v_col_width := NVL(TO_NUMBER(getStringElement(p_worksheet_data(ws_index).col_width_list,colnum)),v_default_col_width);

       resultDocument.defineColumn(p_width=>v_col_width);
     END LOOP;

     -- Sheet Title Row
     v_title := p_worksheet_data(ws_index).title;
     IF v_title.title IS NOT NULL THEN

        IF v_title.cell_span IS NULL OR v_title.cell_span >= p_worksheet_data(ws_index).col_count THEN

            v_title.cell_span := p_worksheet_data(ws_index).col_count-1;

        END IF;

        resultDocument.rowOpen;
        resultDocument.addCell(p_style=>UPPER(v_title.style),p_data=>v_title.title,p_custom_attr=>'ss:MergeAcross="'||v_title.cell_span||'"');
        resultDocument.rowClose;

     END IF;

     -- Caption Row

     IF p_worksheet_data(ws_index).col_caption IS NOT NULL THEN

       resultDocument.rowOpen;
       FOR colnum IN 1 .. p_worksheet_data(ws_index).col_count LOOP

         resultDocument.addCell(p_style=>'ColumnHeader',p_data=>getStringElement(p_worksheet_data(ws_index).col_caption,colnum));

       END LOOP;

       resultDocument.rowClose;

     END IF;


     -- Heading Row
     resultDocument.rowOpen;

     FOR colnum IN 1 .. p_worksheet_data(ws_index).col_count LOOP

       resultDocument.addCell(p_style=>'ColumnHeader',p_data=>getStringElement(p_worksheet_data(ws_index).col_header_list,colnum));

     END LOOP;

     resultDocument.rowClose;

     v_results := buildDataSet(p_worksheet_data(ws_index).query,
                               p_worksheet_data(ws_index).col_count);

     v_count_rows := v_results.COUNT;

     FOR r_index IN 1 .. v_results.COUNT LOOP

        resultDocument.rowOpen;

        v_row  := v_results(r_index);

        FOR c_index IN 1 .. v_row.COUNT LOOP

           v_data_type  := NVL(getStringElement(p_worksheet_data(ws_index).col_datatype_list,c_index),v_default_data_type);

           v_data_style := NVL(UPPER(getStringElement(p_worksheet_data(ws_index).col_style_list,c_index)),NULL);

           IF INSTR(v_style_list,v_data_style) = 0 THEN

              v_data_style := NULL;

           END IF;

           resultDocument.addCell(p_data      => v_row(c_index),
                                  p_data_type => v_data_type,
                                  p_style     => v_data_style);

        END LOOP;

        v_row.DELETE;

        resultDocument.rowClose;

     END LOOP;

     v_results.DELETE;

     -- Formula Row
     IF p_worksheet_data(ws_index).col_formula_list IS NOT NULL THEN

        resultDocument.rowOpen;

        FOR colnum IN 1 .. p_worksheet_data(ws_index).col_count LOOP

          v_data_style := NVL(UPPER(getStringElement(p_worksheet_data(ws_index).col_style_list,colnum)),NULL);
          v_formula := replace(getStringElement(p_worksheet_data(ws_index).col_formula_list,colnum),'<ZMIN>',trim(to_char(v_count_rows)));
          resultDocument.addCell(p_formula   => v_formula,
                                 p_data_type => v_data_type,
                                 p_style     => v_data_style);

        END LOOP;

        resultDocument.rowClose;

     END IF;

     resultDocument.worksheetClose;

  END LOOP;

  resultDocument.documentClose;

  RETURN resultDocument;

END;


/*=============*/
/* END PACKAGE */
/*=============*/
END;
/

