create or replace procedure lst_SPT943si3(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('УЬгн');
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si3spt943(node_name(x),sdate,edate);
              END LOOP;
END;
/

