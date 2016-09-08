create or replace procedure lst_spt960si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('АТС 368'--,
                               --'АТС 776'),-- Убрал 11.09.2008 замена ТВ на СПТ943
                               --'АТС 543');--20.06.2008 замена прибора, установлен СПТ943
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

