create or replace procedure lst_spt960si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('��� 368'--,
                               --'��� 776'),-- ����� 11.09.2008 ������ �� �� ���943
                               --'��� 543');--20.06.2008 ������ �������, ���������� ���943
                               );
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2spt960(node_name(x),sdate,edate);
              END LOOP;
END;
/

