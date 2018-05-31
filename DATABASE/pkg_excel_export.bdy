create or replace package body pkg_excel_export is
/*
  -- Private type declarations
  type <TypeName> is <Datatype>;
  
  -- Private constant declarations
  <ConstantName> constant <Datatype> := <Value>;

  -- Private variable declarations
  <VariableName> <Datatype>;

  -- Function and procedure implementations
  function <FunctionName>(<Parameter> <Datatype>) return <Datatype> is
    <LocalVariable> <Datatype>;
  begin
    <Statement>;
    return(<Result>);
  end;

begin
  -- Initialization
  <Statement>;
*/  
  PROCEDURE excel_open(l_xml_body IN OUT NOCOPY CLOB) IS
     BEGIN

          l_xml_body := '<?xml version="1.0" encoding="ISO-8859-9"?>' || chr(10) ||
                        '<Workbook xmlns="urn:schemas-microsoft-com:office:spreadsheet"' ||
                        chr(10) ||
                        'xmlns:o="urn:schemas-microsoft-com:office:office"' ||
                        chr(10) ||
                        'xmlns:x="urn:schemas-microsoft-com:office:excel"' ||
                        chr(10) ||
                        'xmlns:ss="urn:schemas-microsoft-com:office:spreadsheet"' ||
                        chr(10) ||
                        'xmlns:html="http://www.w3.org/TR/REC-html40">' ||
                        chr(10) ||
                        '<ExcelWorkbook xmlns="urn:schemas-microsoft-com:office:excel">' ||
                        chr(10) || '<WindowHeight>8580</WindowHeight>' ||
                        chr(10) || '<WindowWidth>15180</WindowWidth>' || chr(10) ||
                        '<WindowTopX>120</WindowTopX>' || chr(10) ||
                        '<WindowTopY>45</WindowTopY>' || chr(10) ||
                        '<ProtectStructure>False</ProtectStructure>' || chr(10) ||
                        '<ProtectWindows>False</ProtectWindows>' || chr(10) ||
                        '</ExcelWorkbook>' || chr(10) || '<Styles>' || chr(10) ||
                        '<Style ss:ID="Default" ss:Name="Normal">' || chr(10) ||
                        '<Alignment ss:Vertical="Bottom"/>' || chr(10) ||
                        '<Borders/>' || chr(10) || '<Font/>' || chr(10) ||
                        '<Interior/>' || chr(10) || '<NumberFormat/>' || chr(10) ||
                        '<Protection/>' || chr(10) || '</Style>' || chr(10) ||
                        '<Style ss:ID="s22">' || chr(10) ||
                        '<Font x:Family="Swiss" ss:Bold="1" ss:Underline="Single"/>' ||
                        chr(10) || '</Style>' || chr(10) || '</Styles>';
     END excel_open;

  /**
  *  Closes the excel file
  * 
  **/
     PROCEDURE excel_close(l_xml_body IN OUT NOCOPY CLOB) IS
     BEGIN
          l_xml_body := l_xml_body || '</Workbook>';
     END excel_close;

  /**
  *  Opens a worksheet in the Excel file.
  *  You may open multiple worksheets.
  **/
     PROCEDURE worksheet_open
     (
          l_xml_body      IN OUT NOCOPY CLOB,
          p_worksheetname IN VARCHAR2
     ) IS
     BEGIN
          --
          -- Create the  worksheet
          --
          l_xml_body := l_xml_body || '<Worksheet ss:Name="' || p_worksheetname ||
                        '"><Table>';
     END worksheet_open;

  /**
  *  Closes the worksheet in the Excel file.
  * 
  **/
     PROCEDURE worksheet_close(l_xml_body IN OUT NOCOPY CLOB) IS
     BEGIN
          l_xml_body := l_xml_body || '</Table></Worksheet>';
     END worksheet_close;

  /**
  *  Opens the row tag
  * 
  **/
     PROCEDURE row_open(l_xml_body IN OUT NOCOPY CLOB) IS
     BEGIN
          l_xml_body := l_xml_body || '<Row>';
     END row_open;

  /**
  *  Closes the row tag
  * 
  **/
     PROCEDURE row_close(l_xml_body IN OUT NOCOPY CLOB) IS
     BEGIN
          l_xml_body := l_xml_body || '</Row>' || chr(10);
     END row_close;

  /**
  *  After opening the row, we can write something the first cell
  *  If you want it blank, write ''
  **/
     PROCEDURE cell_write
     (
          l_xml_body IN OUT NOCOPY CLOB,
          p_content  IN VARCHAR2
     ) IS
     BEGIN
          l_xml_body := l_xml_body || '<Cell><Data ss:Type="String"> ' ||
                        p_content || ' </Data></Cell>';
     END cell_write;

  /**
  *  If you are using this package from APEX, you get download the excel file.
  * 
  **/
     PROCEDURE excel_get
     (
          l_xml_body IN OUT NOCOPY CLOB,
          p_filename IN VARCHAR2
     ) IS
          xx BLOB;
          do NUMBER;
          so NUMBER;
          bc NUMBER;
          lc NUMBER;
          w  NUMBER;
     BEGIN

          dbms_lob.createtemporary(xx, TRUE);
          do := 1;
          so := 1;
          bc := dbms_lob.default_csid;
          lc := dbms_lob.default_lang_ctx;
          w  := dbms_lob.no_warning;

          dbms_lob.converttoblob(xx,
                                 l_xml_body,
                                 dbms_lob.lobmaxsize,
                                 do,
                                 so,
                                 bc,
                                 lc,
                                 w);

          owa_util.mime_header('application/octet', FALSE);

          -- set the size so the browser knows how much to download
          htp.p('Content-length: ' || dbms_lob.getlength(xx));
          -- the filename will be used by the browser if the users does a save as
          htp.p('Content-Disposition:  attachment; filename="' || p_filename ||
                '.xml' || '"');
          -- close the headers
          owa_util.http_header_close;
          -- download the BLOB
          wpg_docload.download_file(xx);
     END excel_get;

  /**
  *  Writes the Excel file to some directory with a name.
  *  This procedure writes the CLOB data to file
  * 
  **/
     PROCEDURE prc_write_file
     (
          p_filename IN VARCHAR2,
          p_dir      IN VARCHAR2,
          p_clob     IN CLOB
     ) IS

          c_amount CONSTANT BINARY_INTEGER := 32767;
          l_buffer   VARCHAR2(32767);
          l_chr10    PLS_INTEGER;
          l_cloblen  PLS_INTEGER;
          l_fhandler utl_file.file_type;
          l_pos      PLS_INTEGER := 1;

     BEGIN

          l_cloblen  := dbms_lob.getlength(p_clob);
          l_fhandler := utl_file.fopen(p_dir, p_filename, 'W', c_amount);

          WHILE l_pos < l_cloblen
          LOOP
               l_buffer := dbms_lob.substr(p_clob, c_amount, l_pos);
               EXIT WHEN l_buffer IS NULL;
               l_chr10 := instr(l_buffer, chr(10), -1);
               IF l_chr10 != 0
               THEN
                    l_buffer := substr(l_buffer, 1, l_chr10 - 1);
               END IF;
               utl_file.put_line(l_fhandler, l_buffer, TRUE);
               l_pos := l_pos + least(length(l_buffer) + 1, c_amount);
          END LOOP;

          utl_file.fclose(l_fhandler);

     EXCEPTION

          --WE SHOULD HANDLE THE FILE EXCEPTIONS HERE!!!!!
          WHEN OTHERS THEN
               IF utl_file.is_open(l_fhandler)
               THEN
                    utl_file.fclose(l_fhandler);
               END IF;
               RAISE;   
     END;

END pkg_excel_export;
  
  
  
----end pkg_excel_export;
/

