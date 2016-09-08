create or replace procedure print_report_t1t2 is

  cursor c_v_hours_for is
  select "����", "����", "�����", "T1", "T2", "�������������" from v_hours_for;

  wfile_handle utl_file.file_type;
  v_wstring varchar2 (200);
  v_header varchar2(100);
  v_file varchar2(100);
  v_date varchar2(20);

begin
  select to_char(sysdate,'dd_mm_yyyy') into v_date from dual;
  v_header :='����'||chr(9)||'����'||chr(9)||'�����'||chr(9)||'T1'||chr(9)||'T2'||chr(9)||'�������������';

    v_file := 'hours_for_'||v_date||'.xls';
    wfile_handle := utl_file.fopen ('EXPORT_DIR',v_file, 'W');
    utl_file.put_line(wfile_handle,v_header);

    for r_h in c_v_hours_for loop
      v_wstring := to_char(r_h."����",'dd.mm.yyyy hh24:mi')||chr(9)
                ||r_h."����"||chr(9)
                ||r_h."�����"||chr(9)
                --||to_char(r.hiredate,'dd/mm/yyyy')||chr(9)
                ||r_h."T1"||chr(9)
                ||r_h."T2"||chr(9)
                ||r_h."�������������";

      utl_file.put_line(wfile_handle,v_wstring);
    end loop;
    utl_file.fclose (wfile_handle);
end print_report_t1t2;
/

