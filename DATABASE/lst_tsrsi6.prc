create or replace procedure lst_tsrsi6(sdate IN varchar,edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('Дом на 7 линии ВО');
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si6tsr(node_name(x),sdate,edate);
              END LOOP;
END;
/

