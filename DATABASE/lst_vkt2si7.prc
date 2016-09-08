create or replace procedure lst_vkt2si7(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('¿“— 441'   -- 23.12.2015
                               );

BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si7vkt2m(node_name(x),sdate,edate);
              END LOOP;
END;
/

