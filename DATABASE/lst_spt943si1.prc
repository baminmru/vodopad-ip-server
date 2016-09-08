create or replace procedure lst_spt943si1(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('АТС 707_9', -- добавил 17.12.2012 ранее узел не контролировался ТГК
                               'АТС 707_13' -- добавил 17.12.2012 ранее узел не контролировался ТГК
                               );

BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si1spt943(node_name(x),sdate,edate);
              END LOOP;
END;
/

