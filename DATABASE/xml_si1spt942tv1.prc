create or replace procedure xml_si1spt942tv1(p IN number,dt IN number,sdate IN varchar2,edate IN varchar2)
AUTHID CURRENT_USER
IS
nodeid          number(4):= p; -- 'АТС 751'  id_bd=12;
day_time        number(1); -- тип архива: 3-час; 4-сут;
/*-----объявление переменных заголовка-------------*/
--nodeid:= p;
n_dogovor    contract.fld33%type:='-'; --номер договора теплоснабжения -- НЕ НУЖЕН!
kod_scheme   contract.fld109%type:='-'; --код схемы измерения
kod_vich     contract.fld51%type:='-'; --тип вычисл.
n_vich       contract.fld12%type:='-'; --номер теловыч. -- НЕ НУЖЕН!
doc_date     varchar2(16);             --дата формирования документа
doc_date2    varchar2(8);              -- дата в формате YYYYMM00 тег day
m_code       varchar2(64);             -- для measuringpoint code имя узла "АТС 751"
m_name       varchar2(64);             -- для measuringpoint name адрес узла "Кузнецова ул. д.28"
sender_name  varchar2(64):='ОСГО АХУ ПФ "ОАО Ростелеком"';
sender_inn   varchar2(10):='7707049388';
saving_time  varchar2(1):= '1';
status_val   varchar(1):= '0';          -- 0-коммерч. инфо, 1-не коммерч. инфо
un_code      varchar2(8):= 'unknown';    -- поле помещаю в необязательный тег <area inn>
/*-----курсор заполнение заголовка-------------*/
  cursor header_1_cur IS
         select c.fld33, -- Договор
                c.fld109, -- Схема
                c.fld51, -- Тип ТВ
                c.fld12, -- № ТВ
                to_char(sysdate,'YYYYMMDDHH24miSS'), -- первый заголовок
                --to_char(sysdate,'YYYYMM')||'00',     -- второй заголовок
                to_char(to_date(sdate,'DD.MM.YYYY'),'YYYYMMDD'),     -- второй заголовок
                bb.cshort,
                bb.caddress
         from contract c, bbuildings bb, bdevices bd
         where bb.id_bu=bd.id_bu AND bd.id_bd=c.id_bd AND c.id_bd = nodeid;
/*---курсор заполнение данными------------------*/
TYPE cdat_typ IS REF CURSOR;
  cdat_tv1 cdat_typ;
-- СИ-1 Дата,Время,M1,V1,t1,P1,W1,  M2,V2,t2,P2,W2,  dM,dW,tхв,Pхв,Tо,Tр,Tоо,НС
  cdata_tv1 varchar2(64);-- 1 время (period start-end)
-- ТВ1 подающая труба
  cM1   varchar2(64);-- 3 datacurr.M1%type;
  cV1   varchar2(64);-- 4 datacurr.V1%type;
  cT1   varchar2(64);-- 5 datacurr.T1%type;
  cP1   varchar2(64);-- 6 datacurr.p1%type;
  cW1   varchar2(64);-- 7 datacurr.q1%type; -- Q --> W тепловая энергии
-- ТВ1 обратная труба
  cM2   varchar2(64);-- 8 datacurr.m2%type;
  cV2   varchar2(64);-- 9
  cT2   varchar2(64);-- 10 datacurr.T2%type;
  cP2   varchar2(64);-- 11
  cW2   varchar2(64);-- 12 datacurr.q2%type; -- Q --> W тепловая энергии


fd utl_file.file_type; -- объявление дескриптора

begin
--nodeid:=p;
day_time:= dt;
open header_1_cur;
fetch header_1_cur into n_dogovor,
                        kod_scheme,
                        kod_vich,
                        n_vich,
                        doc_date,
                        doc_date2,
                        m_code,
                        m_name;
                       -- sender_name,
                       -- sender_inn,
                       -- saving_time;

/* предполагается, что данные будут отсылаться порциями за сутки. В имени файла указывается 
   дата суток и идентификаор узла (id_bd) */
fd:= utl_file.fopen('d:\database\utl', doc_date2||'_'||to_char(nodeid)||'.xml', 'w');
-- fd:= utl_file.fopen('d:\database\utl', to_char(sysdate-1,'DD-MON-YY')||'_'||to_char(nodeid)||'_'||doc_date2||'.xml', 'w');
-- fd:= utl_file.fopen('D:\db\utl', nodename||'.txt', 'w'); -- домашний каталог

 utl_file.put_line(fd,'<?xml version="1.0" encoding="windows-1251" ?>');
 utl_file.put_line(fd,'<message class="CAE020" version="1" number="1">');
-- заголовок datetime
 utl_file.new_line(fd); -- удалить

 utl_file.put_line(fd,'<datetime>');
 utl_file.put_line(fd,'<timestamp>'||doc_date||'</timestamp>');
 utl_file.put_line(fd,'<daylightsavingtime>'||saving_time||'</daylightsavingtime>');
 utl_file.put_line(fd,'<day>'||doc_date2||'</day>');
-- utl_file.put_line(fd,'<day>'||sdate||'</day>');
 utl_file.put_line(fd,'</datetime>');
-- заголовок sender
 utl_file.new_line(fd); -- удалить
-- 11.03.2013 просьба поменять местами теги <sender_name>,<sender_inn>
 utl_file.put_line(fd,'<sender>');
 utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'<inn>'||sender_inn||'</inn>');
-- utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'</sender>');

 utl_file.new_line(fd); -- удалить

 utl_file.put_line(fd,'<area>');
-- utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'<inn>'||sender_inn||'</inn>');
 utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'<measuringpoint code='||'"'||m_code||'"'||' '||'name='||'"'||kod_vich||'"'||'>'); --name=тип ТВ
-- utl_file.put_line(fd,'<measuringpoint code='||'"'||m_code||'"'||' '||'name='||'"'||m_name||'"'||'>'); --name=АТС 751
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/****************************************************************************/

/************* курсор: Подготовка и вставка данных M1 ТВ1 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.M1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="M1_1"'||' '||'desc="Массовый расход в подающем трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               cM1;--,cV1,cT1,cP1,cW1;--, --ТВ1
--               cM2,cV2,cT2,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
                              TO_CHAR(cM1)    --||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных V1 ТВ1 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.V1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="V1_1"'||' '||'desc="Объемный расход в подающем трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1;
               cV1;--,cT1,cP1,cW1;--, --ТВ1
--               cM2,cV2,cT2,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
                              TO_CHAR(cV1)    --||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных T1 ТВ1 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.T1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="T1_1"'||' '||'desc="Температура в подающем трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1;--,
               cT1; --,cP1,cW1;--, --ТВ1
--               cM2,cV2,cT2,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
                              TO_CHAR(cT1)    --||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных P1 ТВ1 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.P1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="P1_1"'||' '||'desc="Давление в подающем трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,
               cP1; --,cW1;--, --ТВ1
--               cM2,cV2,cT2,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
                              TO_CHAR(cP1)    --||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных W1 (Q1) ТВ1 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.Q1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="Q_1"'||' '||'desc="Потребление тепловой энергии, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,
               cW1;--, --ТВ1
--               cM2,cV2,cT2,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных M2 ТВ2 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.M2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="M2_1"'||' '||'desc="Массовый расход в обратном трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --ТВ1
               cM2;
                 --,cV2,cT2,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
                              TO_CHAR(cM2)    --||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных V2 ТВ2 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.V2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="V2_1"'||' '||'desc="Объемный расход в обратном трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --ТВ1
               --cM2,
               cV2; --,cT2,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
                              TO_CHAR(cV2)    --||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных T2 ТВ2 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.T2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="T2_1"'||' '||'desc="Температура в обратном трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --ТВ1
               --cM2,cV2; --,
               cT2; --,cP2,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
                              TO_CHAR(cT2)    --||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
/************* курсор: Подготовка и вставка данных P2 ТВ2 *******************/
OPEN cdat_tv1 FOR
     select -- ТВ2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.P2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 тепловая энергия
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ТВ1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="P2_1"'||' '||'desc="Давление в обратном трубопроводе, канал 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --ТВ1
               --cM2,cV2,cT2; --,
               cP2; --,cW2; --, --ТВ2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 добавил значения
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
                              TO_CHAR(cP2)    --||chr(9)||--P2 2008.06.20 добавил значения
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');


utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить
utl_file.new_line(fd); -- удалить

--utl_file.put_line(fd,'------------------------------------------------------'); -- удалить
utl_file.new_line(fd); -- удалить
-- utl_file.put_line(fd,'</measuringchannel>');
utl_file.put_line(fd,'</measuringpoint>'); --name=АТС 751


 utl_file.new_line(fd); -- удалить
 utl_file.new_line(fd); -- удалить
 utl_file.new_line(fd); -- удалить


 utl_file.put_line(fd,'</area>');
 utl_file.put_line(fd,'</message>');
 utl_file.fclose(fd);
close header_1_cur;
CLOSE cdat_tv1;

end;
/

