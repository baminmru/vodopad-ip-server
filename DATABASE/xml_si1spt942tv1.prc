create or replace procedure xml_si1spt942tv1(p IN number,dt IN number,sdate IN varchar2,edate IN varchar2)
AUTHID CURRENT_USER
IS
nodeid          number(4):= p; -- '��� 751'  id_bd=12;
day_time        number(1); -- ��� ������: 3-���; 4-���;
/*-----���������� ���������� ���������-------------*/
--nodeid:= p;
n_dogovor    contract.fld33%type:='-'; --����� �������� �������������� -- �� �����!
kod_scheme   contract.fld109%type:='-'; --��� ����� ���������
kod_vich     contract.fld51%type:='-'; --��� ������.
n_vich       contract.fld12%type:='-'; --����� �������. -- �� �����!
doc_date     varchar2(16);             --���� ������������ ���������
doc_date2    varchar2(8);              -- ���� � ������� YYYYMM00 ��� day
m_code       varchar2(64);             -- ��� measuringpoint code ��� ���� "��� 751"
m_name       varchar2(64);             -- ��� measuringpoint name ����� ���� "��������� ��. �.28"
sender_name  varchar2(64):='���� ��� �� "��� ����������"';
sender_inn   varchar2(10):='7707049388';
saving_time  varchar2(1):= '1';
status_val   varchar(1):= '0';          -- 0-�������. ����, 1-�� �������. ����
un_code      varchar2(8):= 'unknown';    -- ���� ������� � �������������� ��� <area inn>
/*-----������ ���������� ���������-------------*/
  cursor header_1_cur IS
         select c.fld33, -- �������
                c.fld109, -- �����
                c.fld51, -- ��� ��
                c.fld12, -- � ��
                to_char(sysdate,'YYYYMMDDHH24miSS'), -- ������ ���������
                --to_char(sysdate,'YYYYMM')||'00',     -- ������ ���������
                to_char(to_date(sdate,'DD.MM.YYYY'),'YYYYMMDD'),     -- ������ ���������
                bb.cshort,
                bb.caddress
         from contract c, bbuildings bb, bdevices bd
         where bb.id_bu=bd.id_bu AND bd.id_bd=c.id_bd AND c.id_bd = nodeid;
/*---������ ���������� �������------------------*/
TYPE cdat_typ IS REF CURSOR;
  cdat_tv1 cdat_typ;
-- ��-1 ����,�����,M1,V1,t1,P1,W1,  M2,V2,t2,P2,W2,  dM,dW,t��,P��,T�,T�,T��,��
  cdata_tv1 varchar2(64);-- 1 ����� (period start-end)
-- ��1 �������� �����
  cM1   varchar2(64);-- 3 datacurr.M1%type;
  cV1   varchar2(64);-- 4 datacurr.V1%type;
  cT1   varchar2(64);-- 5 datacurr.T1%type;
  cP1   varchar2(64);-- 6 datacurr.p1%type;
  cW1   varchar2(64);-- 7 datacurr.q1%type; -- Q --> W �������� �������
-- ��1 �������� �����
  cM2   varchar2(64);-- 8 datacurr.m2%type;
  cV2   varchar2(64);-- 9
  cT2   varchar2(64);-- 10 datacurr.T2%type;
  cP2   varchar2(64);-- 11
  cW2   varchar2(64);-- 12 datacurr.q2%type; -- Q --> W �������� �������


fd utl_file.file_type; -- ���������� �����������

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

/* ��������������, ��� ������ ����� ���������� �������� �� �����. � ����� ����� ����������� 
   ���� ����� � ������������ ���� (id_bd) */
fd:= utl_file.fopen('d:\database\utl', doc_date2||'_'||to_char(nodeid)||'.xml', 'w');
-- fd:= utl_file.fopen('d:\database\utl', to_char(sysdate-1,'DD-MON-YY')||'_'||to_char(nodeid)||'_'||doc_date2||'.xml', 'w');
-- fd:= utl_file.fopen('D:\db\utl', nodename||'.txt', 'w'); -- �������� �������

 utl_file.put_line(fd,'<?xml version="1.0" encoding="windows-1251" ?>');
 utl_file.put_line(fd,'<message class="CAE020" version="1" number="1">');
-- ��������� datetime
 utl_file.new_line(fd); -- �������

 utl_file.put_line(fd,'<datetime>');
 utl_file.put_line(fd,'<timestamp>'||doc_date||'</timestamp>');
 utl_file.put_line(fd,'<daylightsavingtime>'||saving_time||'</daylightsavingtime>');
 utl_file.put_line(fd,'<day>'||doc_date2||'</day>');
-- utl_file.put_line(fd,'<day>'||sdate||'</day>');
 utl_file.put_line(fd,'</datetime>');
-- ��������� sender
 utl_file.new_line(fd); -- �������
-- 11.03.2013 ������� �������� ������� ���� <sender_name>,<sender_inn>
 utl_file.put_line(fd,'<sender>');
 utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'<inn>'||sender_inn||'</inn>');
-- utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'</sender>');

 utl_file.new_line(fd); -- �������

 utl_file.put_line(fd,'<area>');
-- utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'<inn>'||sender_inn||'</inn>');
 utl_file.put_line(fd,'<name>'||sender_name||'</name>');
 utl_file.put_line(fd,'<measuringpoint code='||'"'||m_code||'"'||' '||'name='||'"'||kod_vich||'"'||'>'); --name=��� ��
-- utl_file.put_line(fd,'<measuringpoint code='||'"'||m_code||'"'||' '||'name='||'"'||m_name||'"'||'>'); --name=��� 751
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/****************************************************************************/

/************* ������: ���������� � ������� ������ M1 ��1 *******************/
OPEN cdat_tv1 FOR
     select -- ��1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.M1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="M1_1"'||' '||'desc="�������� ������ � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               cM1;--,cV1,cT1,cP1,cW1;--, --��1
--               cM2,cV2,cT2,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
                              TO_CHAR(cM1)    --||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ V1 ��1 *******************/
OPEN cdat_tv1 FOR
     select -- ��1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.V1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="V1_1"'||' '||'desc="�������� ������ � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1;
               cV1;--,cT1,cP1,cW1;--, --��1
--               cM2,cV2,cT2,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
                              TO_CHAR(cV1)    --||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ T1 ��1 *******************/
OPEN cdat_tv1 FOR
     select -- ��1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.T1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="T1_1"'||' '||'desc="����������� � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1;--,
               cT1; --,cP1,cW1;--, --��1
--               cM2,cV2,cT2,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
                              TO_CHAR(cT1)    --||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ P1 ��1 *******************/
OPEN cdat_tv1 FOR
     select -- ��1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.P1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="P1_1"'||' '||'desc="�������� � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,
               cP1; --,cW1;--, --��1
--               cM2,cV2,cT2,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
                              TO_CHAR(cP1)    --||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ W1 (Q1) ��1 *******************/
OPEN cdat_tv1 FOR
     select -- ��1 - M1,V1,t1,P1,W1,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.Q1,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="Q_1"'||' '||'desc="����������� �������� �������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,
               cW1;--, --��1
--               cM2,cV2,cT2,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
                              TO_CHAR(cW1)    --||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ M2 ��2 *******************/
OPEN cdat_tv1 FOR
     select -- ��2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.M2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="M2_1"'||' '||'desc="�������� ������ � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --��1
               cM2;
                 --,cV2,cT2,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
                              TO_CHAR(cM2)    --||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ V2 ��2 *******************/
OPEN cdat_tv1 FOR
     select -- ��2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.V2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="V2_1"'||' '||'desc="�������� ������ � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --��1
               --cM2,
               cV2; --,cT2,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
                              TO_CHAR(cV2)    --||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ T2 ��2 *******************/
OPEN cdat_tv1 FOR
     select -- ��2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.T2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="T2_1"'||' '||'desc="����������� � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --��1
               --cM2,cV2; --,
               cT2; --,cP2,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
                              TO_CHAR(cT2)    --||chr(9)||--T2
--                              TO_CHAR(cP2)    ||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
/************* ������: ���������� � ������� ������ P2 ��2 *******************/
OPEN cdat_tv1 FOR
     select -- ��2 - M2,V2,t2,P2,W2,
       '<period start='||'"DT'||TO_CHAR((c.dcounter-1/24)+1/24/60/60,'YYYYMMDDHH24MISS')||'"'
                ||' '||'end='||'"DT'||TO_CHAR(c.dcounter,'YYYYMMDDHH24MISS')||'"'||'>', --1
       '<value status="0">'||NVL(TO_CHAR(trunc(c.P2,3)),'-')||'</value>'               --2

--       '<V1_1>'||NVL(TO_CHAR(trunc(c.v1,2)),'-')||'</V1_1>',               --3
--       '<T1_1>'||NVL(TO_CHAR(trunc(c.t1,3)),'-')||'</T1_1>',               --4
--       '<P1_1>'||NVL(TO_CHAR(trunc(c.P1)),'-')||'</P1_1>',                 --5
--       '<Q1_1>'||NVL(TO_CHAR(trunc(c.q1,3)),'-')||'</Q1_1>'                --6 W1 �������� �������
       ||'</period>'||chr(13)--||'</measuringchannel>'
from datacurr c
   where c.id_bd = nodeid AND c.id_ptype=day_time --3 --
     AND c.dcounter>=to_date(sdate||' 00:00:01','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:59:59','DD.MM.YYYY HH24:MI:SS');--+(60/1440);

/* ��1 =================================================================================== */
utl_file.put_line(fd,'<measuringchannel code="P2_1"'||' '||'desc="�������� � �������� ������������, ����� 1">');
     LOOP
         FETCH cdat_tv1 INTO
               cdata_tv1,--chour,
               --cM1,cV1,cT1,cP1,cW1;--, --��1
               --cM2,cV2,cT2; --,
               cP2; --,cW2; --, --��2
              -- cdM,cdW,/*cM1g,cV1g,cT1g,cP1g,cW1g, */cTxv,cPxv,cT_o, cTp,cToo,/*cTog, cHC1,*/cHC2;

         EXIT WHEN cdat_tv1%NOTFOUND;
         utl_file.put_line(fd,
                              cdata_tv1       ||chr(13)||
--                              TO_CHAR(cV1)    ||chr(9)||--M1
--                              TO_CHAR(cV1)    ||chr(9)||--V1 �������� �� M1?
--                              TO_CHAR(cT1)    ||chr(9)||--T1
--                              TO_CHAR(cP1)    ||chr(9)||--P1 2008.06.20 ������� ��������
--                              TO_CHAR(cW1)    ||chr(9)||--Q1
                              ------ M2,V2,t2,P2,W2,
--                              TO_CHAR(cM2)    ||chr(9)||--M2
--                              TO_CHAR(cV2)    ||chr(9)||--V2
--                              TO_CHAR(cT2)    ||chr(9)||--T2
                              TO_CHAR(cP2)    --||chr(9)||--P2 2008.06.20 ������� ��������
--                              TO_CHAR(cW2)    --||chr(9)||--Q2
                             );
     END LOOP;
utl_file.put_line(fd,'</measuringchannel>');


utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������
utl_file.new_line(fd); -- �������

--utl_file.put_line(fd,'------------------------------------------------------'); -- �������
utl_file.new_line(fd); -- �������
-- utl_file.put_line(fd,'</measuringchannel>');
utl_file.put_line(fd,'</measuringpoint>'); --name=��� 751


 utl_file.new_line(fd); -- �������
 utl_file.new_line(fd); -- �������
 utl_file.new_line(fd); -- �������


 utl_file.put_line(fd,'</area>');
 utl_file.put_line(fd,'</message>');
 utl_file.fclose(fd);
close header_1_cur;
CLOSE cdat_tv1;

end;
/

