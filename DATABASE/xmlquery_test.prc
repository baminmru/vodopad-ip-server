create or replace procedure xmlquery_test
AS
  v_file  UTL_FILE.file_type;
  v_xml   CLOB;
  v_more  BOOLEAN := TRUE;
BEGIN
  -- Create XML document from query.
  -- select "Дата","Узел","Адрес",T1,T2 from v_hours_for
  -- v_xml := DBMS_XMLQUERY.getxml('SELECT table_name, tablespace_name FROM user_tables WHERE rownum & 6');
  v_xml := DBMS_XMLQUERY.getxml('select T1,T2 from v_hours_for');

  -- Output XML document to file.
  v_file := UTL_FILE.fopen('D:\database\utl\', 'xmlquery_test1.xml', 'w');
  WHILE v_more LOOP
    UTL_FILE.put(v_file, Substr(v_xml, 1, 32767));
    IF LENGTH(v_xml) > 32767 THEN
      v_xml :=  SUBSTR(v_xml, 32768);
    ELSE
      v_more := FALSE;
    END IF;
  END LOOP;
  UTL_FILE.fclose(v_file);

EXCEPTION
  WHEN OTHERS THEN
    DBMS_OUTPUT.put_line(Substr(SQLERRM,1,255));
    UTL_FILE.fclose(v_file);
END;
/

