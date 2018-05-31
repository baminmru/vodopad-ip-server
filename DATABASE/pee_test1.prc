create or replace procedure pee_test1
AS
     myexcelcontent CLOB;

--     select "Дата", T1, T2 from v_hours_for;
     TYPE data_cur_type IS REF CURSOR;
          data_cur data_cur_type;
          cdate varchar2(20);
          ct1   varchar2(20);
          ct2   varchar2(20);

BEGIN
     -- Test statements here

     --open the file
     pkg_excel_export.excel_open(myexcelcontent);

     --open a worksheet Не понимает кодировку!
     pkg_excel_export.worksheet_open(myexcelcontent, 'Температура Т1, Т2');

     OPEN data_cur FOR
          select "Дата", T1, T2 from v_hours_for;

     LOOP
       FETCH data_cur INTO
             cdate, ct1, ct2;
       EXIT WHEN data_cur%NOTFOUND;

       --open the row
         pkg_excel_export.row_open(myexcelcontent);
         pkg_excel_export.cell_write(myexcelcontent, cdate);
         pkg_excel_export.cell_write(myexcelcontent, ct1);
         pkg_excel_export.cell_write(myexcelcontent, ct2);
         pkg_excel_export.row_close(myexcelcontent);

     END LOOP;

     CLOSE data_cur;
     --close the worksheet
     pkg_excel_export.worksheet_close(myexcelcontent);

     -- Открыть второй лист test2
     pkg_excel_export.worksheet_open(myexcelcontent, 'test2');

    --open the row
     pkg_excel_export.row_open(myexcelcontent);
     pkg_excel_export.cell_write(myexcelcontent,'My First Cell in the Second Row');
     pkg_excel_export.row_close(myexcelcontent);
     pkg_excel_export.worksheet_close(myexcelcontent);

     --close the file
     pkg_excel_export.excel_close(myexcelcontent);

     --write the file somewhere
     pkg_excel_export.prc_write_file(p_filename => 'pkg_excel_exp.xls'
                                     --,p_dir      => 'MY_ORACLE_DIR'
                                     ,p_dir      => 'D:\database\utl'
                                     ,p_clob     => myexcelcontent);

     dbms_output.put_line(substr(myexcelcontent, 1, 10000));

END;
/

