create or replace procedure lst_TSRsi5(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('��� 700');
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si5tsr(node_name(x),sdate,edate);-- ������ �� ��-5
              END LOOP;
END;
/

