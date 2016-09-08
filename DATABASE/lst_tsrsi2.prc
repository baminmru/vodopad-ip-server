create or replace procedure lst_TSRsi2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst(--'��� 750', -- ����� 11.09.2008 ������ �� �� ���943
                               --'��� 784'  -- ����� 02.10.2008 ������ �� �� ���943
                               ); -- 2 ����
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2tsr(node_name(x),sdate,edate);
              END LOOP;
END;
/

