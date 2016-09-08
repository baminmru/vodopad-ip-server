create or replace procedure lst_spt943si2tv2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('��� 225',
                               '��� 271',    -- ������� 2012.12.29
                               '��� 272',    -- ������� 2013.02.14
                               '��� 272-2',  -- ������� 19.12.2007
                               '��� 375',    -- 29.10.2013
                               '��� 585',    -- 28.11.2013
                               '��� 726',     -- 28.11.2013
                               '��� 757'
                               );

BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2spt943tv2(node_name(x),sdate,edate);
              END LOOP;
END;
/

