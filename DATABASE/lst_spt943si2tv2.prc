create or replace procedure lst_spt943si2tv2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('АТС 225',
                               'АТС 271',    -- добавил 2012.12.29
                               'АТС 272',    -- добавил 2013.02.14
                               'АТС 272-2',  -- добавил 19.12.2007
                               'АТС 375',    -- 29.10.2013
                               'АТС 585',    -- 28.11.2013
                               'АТС 726',     -- 28.11.2013
                               'АТС 757'
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

