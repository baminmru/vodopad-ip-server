create or replace procedure lst_spt942si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst(--'¿“— 251',--'¿“— 271',   -- Á‡ÏÂÌ‡ “¬ —œ“943
                               --'¿“— 272',--'¿“— 272-2', -- Á‡ÏÂÌ‡ “¬ —œ“943
                               '¿“— 310','¿“— 371',
                               '¿“— 351','¿“— 356',
                               --'¿“— 375',
                               '¿“— 379',
                               '¿“— 388','¿“— 542',
                               '¿“— 751','¿“— 772',
                               '¿“— 774','¿“— 786',
                               '¿“— 540'--,'√Ã”›'
                               );-- 16 ÛÁÎÓ‚
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2spt942(node_name(x),sdate,edate);
              END LOOP;
END;
/

