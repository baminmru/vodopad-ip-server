create or replace force view v_hours_for as
select distinct /* bb.id_bu, */ da.dcounter as "Дата", bb.cshort as "Узел",
       bb.caddress as "Адрес",
       trunc(da.t1,2) as "T1",
       trunc(da.t2,2) as "T2",
       trunc(da.t3,2) as "T3",
       trunc(da.t4,2) as "T4",
       trunc(da.t5,2) as "T5",
       bb.cfio1 as "Ответственный"
from bbuildings bb, bdevices bd, datacurr da
       where bd.id_bd IN (
       105,90,27,2,51,46,40,39,54,20,85,96,110,141,94,99,119,107,86,89,55,58,38,95,109,37,49,43,36,57,148,
153,53,108,147,16,101,34,100,98,18,50,88,160,42,149,48,45,47,91,31,28,33,59,92,41,10,3,1,6,104,87, -- 62
97,113,11,9,117,114,8,5,4,7,118,84,163,29,35,30,155,116,32,161,162,56,15,21,22,14,19,24,12,26,17,25, -- 94
60,63,64,61,23,13,150,103,106,52,151,111) -- 106
       AND bb.id_bu = bd.id_bu
       AND bd.id_bd = da.id_bd
       AND da.id_ptype = 3
--       AND da.dcounter>=to_date('20.06.2015 07:00:00','DD.MM.YYYY HH24:MI:SS')
--       AND da.dcounter<=to_date('20.06.2015 07:59:59','DD.MM.YYYY HH24:MI:SS')
       AND da.dcounter>=to_date(to_char(sysdate,'DD.MM.YYYY')||' 07:00:00','DD.MM.YYYY HH24:MI:SS')
       AND da.dcounter<=to_date(to_char(sysdate,'DD.MM.YYYY')||' 07:59:59','DD.MM.YYYY HH24:MI:SS')
       GROUP by bb.cshort, bb.id_bu,da.t1,da.t2,da.t3,da.t4,da.t5,da.dcounter, bb.cfio1,bb.caddress
       ORDER BY "Узел"
;

