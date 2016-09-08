create or replace procedure lst_spt942si2gv4(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('АТС 251'
                               );-- 1 узел ГВС по 5 каналу (07.04.2016 узел может быть помещен в si2spt942)
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2spt942gv4(node_name(x),sdate,edate);
              END LOOP;
END;
/

