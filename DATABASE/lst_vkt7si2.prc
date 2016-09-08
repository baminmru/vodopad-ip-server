create or replace procedure lst_vkt7si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('Верейская 34-36'   -- 26.12.2013
                               );

BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2vkt7(node_name(x),sdate,edate);
              END LOOP;
END;
/

