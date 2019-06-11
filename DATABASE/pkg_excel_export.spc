create or replace package pkg_excel_export is

  -- Author  : PITER
  -- Created : 21.11.2016 18:24:55
  -- Purpose : 
  
  -- Public type declarations
  ----type <TypeName> is <Datatype>;
  
  -- Public constant declarations
  ----<ConstantName> constant <Datatype> := <Value>;

  -- Public variable declarations
  ----<VariableName> <Datatype>;

  -- Public function and procedure declarations
--  function <FunctionName>(<Parameter> <Datatype>) return <Datatype>;
    PROCEDURE excel_open(l_xml_body IN OUT NOCOPY CLOB);
     PROCEDURE excel_close(l_xml_body IN OUT NOCOPY CLOB);

     PROCEDURE worksheet_open
     (
          l_xml_body      IN OUT NOCOPY CLOB,
          p_worksheetname IN VARCHAR2
     );
     PROCEDURE worksheet_close(l_xml_body IN OUT NOCOPY CLOB);

     PROCEDURE row_open(l_xml_body IN OUT NOCOPY CLOB);
     PROCEDURE row_close(l_xml_body IN OUT NOCOPY CLOB);

     PROCEDURE cell_write
     (
          l_xml_body IN OUT NOCOPY CLOB,
          p_content  IN VARCHAR2
     );

     PROCEDURE excel_get
     (
          l_xml_body IN OUT NOCOPY CLOB,
          p_filename IN VARCHAR2
     );
     PROCEDURE prc_write_file
     (
          p_filename IN VARCHAR2,
          p_dir      IN VARCHAR2,
          p_clob     IN CLOB
     );
  

end pkg_excel_export;
/

