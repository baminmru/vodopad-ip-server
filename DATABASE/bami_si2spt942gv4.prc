create or replace procedure bami_si2spt942gv4(p IN varchar2,sdate IN varchar2,edate IN varchar2)
AUTHID CURRENT_USER
IS
nodename          varchar2(24);--:= 'АТС 751';
--nodename = p;
n_dogovor    contract.fld12%type:='-'; --номер договора теплоснабжения $$$1$$$
kod_scheme   contract.fld109%type:='-'; --код схемы измерения $$$2$$$
kod_vich     contract.fld51%type:='-'; --код типа вычисл. $$$3$$$
n_vich       contract.fld12%type:='-'; --номер теловыч. $$$4$$$
/*----код типа преобразователя-----------------*/
kod_tipa_preobraz_M1   contract.fld40%type;
kod_tipa_preobraz_M1g   contract.fld41%type;
kod_tipa_preobraz_M2g   contract.fld73%type;
kod_tipa_preobraz_M2   contract.fld104%type;
/*-----условный диаметр------------------------*/
us_diam_M1g      contract.fld16%type;
us_diam_M2       contract.fld17%type;
us_diam_M1       contract.fld18%type;
us_diam_M2g      contract.fld95%type;
/*-----цена импульса---------------------------*/
imp_M1      contract.fld96%type;
imp_M2      contract.fld97%type;
imp_M1g      contract.fld98%type;
imp_M2g      contract.fld99%type;
/*-----нижняя граница диапазона----------------*/
n_gran_M1     contract.fld27%type;
n_gran_M2     contract.fld26%type;
n_gran_M1g     contract.fld25%type;
n_gran_M2g     contract.fld23%type;
/*-----верхняя граница-------------------------*/
u_gran_M1     contract.fld29%type;
u_gran_M2     contract.fld28%type;
u_gran_M1g     contract.fld21%type;
u_gran_M2g     contract.fld96%type;
/*-------погрешность измерений-----------------*/
pogr_M1     contract.fld100%type;
pogr_M2     contract.fld101%type;
pogr_M1g     contract.fld102%type;
pogr_M2g     contract.fld103%type;
/*-----курсор заполнение заголовка-------------*/
  cursor header_1_cur IS
         select c.fld33,c.fld109,c.fld51,c.fld12, -- первый заголовок (договор,схема,наименование,счетчика,№прибора)
                c.fld40,c.fld41,c.fld73,c.fld104, -- код типа преобразоват.(расходомер,расходомер ГВС,расходомер ГВСц,расходомер М2)
                c.fld16,c.fld17,c.fld18,c.fld95,  -- условный диаметр(ДуГВ,ДуОБР,ДуПР,ДуГВСц)
                c.fld96,c.fld97,c.fld98,c.fld99, --цена импульса
                c.fld27,c.fld26,c.fld25,c.fld23, -- нижняя граница диапазона
                c.fld29,c.fld28,c.fld21,c.fld96, -- верхняя граница
                c.fld100,c.fld101,c.fld102,c.fld103 --погрешность измерений
         from bdevices d, contract c, bbuildings b
         where c.id_bd = d.id_bd AND d.id_bu = b.id_bu
--         AND b.cshort = 'АТС 751';--'АТС 750';
         AND b.cshort = nodename;--'АТС 750';
/*---курсор заполнение данными------------------*/
TYPE cdat_typ IS REF CURSOR;
  cdat cdat_typ;
--  cdata datacurr.dcounter%type;
--  chour datacurr.dcounter%type;
  cdata varchar2(12);
  chour varchar2(12);
  cM1   varchar2(10);--datacurr.M1%type;
  cV1   varchar2(10);--datacurr.V1%type;
  cT1   varchar2(10);--datacurr.T1%type;
  cP1   varchar2(10);--datacurr.p1%type;
  cW1   varchar2(10);--datacurr.q1%type; -- Q --> W тепловая энергии
  cM2   varchar2(10);--datacurr.m2%type;
  cT2   varchar2(10);--datacurr.T2%type;
  cV2   varchar2(10);
  cP2   varchar2(10);
  cW2   varchar2(10);--datacurr.q2%type; -- Q --> W тепловая энергии
  cdM   varchar2(10);--datacurr.dm12%type;
  cdW   varchar2(10);--datacurr.dg12%type;
  cM1g  varchar2(10);--datacurr.m3%type; --M1гвс m3 -> M1гвс ??????????????????????????????????????????????
  cV1g  varchar2(10);--datacurr.V3%type; -- V3 -> V1гвс ?????????????????????
  cT1g  varchar2(10);--datacurr.t3%type; -- T3 -> T1гвс ????????????????????
  cP1g  varchar2(10);--datacurr.p3%type;
  cW1g  varchar2(10);--datacurr.q3%type;
  cTxv  varchar2(10);--datacurr.tcool%type; --TCool -> tхв ??????????
  cPxv  varchar2(10);--datacurr.pxb%type; -- PXB -> Pхв ?????
  cT_o  varchar2(10);
  cTp   varchar2(10);
  cToo  varchar2(10);
  cTog  varchar2(10);
--  cHC1   datacurr.Hc_1%type;
  cHC2   datacurr.Hc_2%type;

fd utl_file.file_type;
--v_in varchar2(100);
begin
nodename:=p;
open header_1_cur;
fetch header_1_cur into n_dogovor,kod_scheme,kod_vich,n_vich,
                        kod_tipa_preobraz_M1,kod_tipa_preobraz_M1g,kod_tipa_preobraz_M2g,kod_tipa_preobraz_M2,
                        us_diam_M1g,us_diam_M2,us_diam_M1,us_diam_M2g,
                        imp_M1,imp_M2,imp_M1g,imp_M2g,
                        n_gran_M1,n_gran_M2,n_gran_M1g,n_gran_M2g,
                        u_gran_M1,u_gran_M2,u_gran_M1g,u_gran_M2g,
                        pogr_M1,pogr_M2,pogr_M1g,pogr_M2g;
 fd:= utl_file.fopen('d:\database\utl', to_char(sysdate-1,'DD-MON')||'_'||nodename||'.txt', 'w');
-- fd:= utl_file.fopen('D:\db\utl', nodename||'.txt', 'w'); -- домашний каталог
 utl_file.put_line(fd,'Номер договора теплоснабжения:'||chr(9)||'$$$1$$$'||n_dogovor);
 utl_file.put_line(fd,'Код схемы измерений:'||chr(9)||'$$$2$$$'||kod_scheme);
 utl_file.put_line(fd,'Код типа тепловычеслителя:'||chr(9)||'$$$3$$$'||kod_vich);
 utl_file.put_line(fd,'Номер тепловычеслителя:'||chr(9)||'$$$4$$$'||n_vich);
-- utl_file.new_line(fd);
--17.01.2008 правка, ошибка в 'код типа преобразователя'
 utl_file.put_line(fd,'Канал измерения массы(объема):'||chr(9)||'$$$11$$$'||'M1'||chr(9)||'V1'||chr(9)||'M2'||chr(9)||'V2'||chr(9)||'M1гв'||chr(9)||'V1гв'||chr(9)||'M2гв'||chr(9)||'V2гв');
 utl_file.put_line(fd,'Код типа преобразователя:'||chr(9)||'$$$12$$$'||kod_tipa_preobraz_M1||chr(9)||'-'||chr(9)||kod_tipa_preobraz_M2||chr(9)||'-'||chr(9)||kod_tipa_preobraz_M1g||chr(9)||'-'||chr(9)||kod_tipa_preobraz_M2g||chr(9)||'-');
 utl_file.put_line(fd,'Условный диаметр, мм:'||chr(9)||'$$$13$$$'||us_diam_M1||chr(9)||'-'||chr(9)||us_diam_M2||chr(9)||'-'||chr(9)||us_diam_M1g||chr(9)||'-'||chr(9)||us_diam_M2g||chr(9)||'-');
 utl_file.put_line(fd,'Цена импульса, л/имп:'||chr(9)||'$$$14$$$'||imp_M1 ||chr(9)||'-'||chr(9)||imp_M2||chr(9)||'-'||chr(9)||imp_M1g||chr(9)||'-'||chr(9)||imp_M2g||chr(9)||'-');
 utl_file.put_line(fd,'Нижняя граница диапазона, т/ч:'||chr(9)||'$$$15$$$'||n_gran_M1||chr(9)||'-'||chr(9)||n_gran_M2||chr(9)||'-'||chr(9)||n_gran_M1g||chr(9)||'-'||chr(9)||n_gran_M2g||chr(9)||'-');
 utl_file.put_line(fd,'Верхняя граница диапазона, т/ч:'||chr(9)||'$$$16$$$'||u_gran_M1||chr(9)||'-'||chr(9)||u_gran_M2||chr(9)||'-'||chr(9)||u_gran_M1g||chr(9)||'-'||chr(9)||n_gran_M2g||chr(9)||'-');
 utl_file.put_line(fd,'Допускаемая погрешность измерений, %:'||chr(9)||'$$$17$$$'||pogr_M1||chr(9)||'-'||chr(9)||pogr_M2||chr(9)||'-'||chr(9)||pogr_M1g||chr(9)||'-'||chr(9)||pogr_M2g||chr(9)||'-');
-- utl_file.new_line(fd);
 utl_file.put_line(fd,'$$$data_start$$$');
-- utl_file.new_line(fd);
/* Вставка данных*/
OPEN cdat FOR
     select
            TO_CHAR(c.dcounter, 'DD.MM.YYYY') as "Дата", --1
            TO_NUMBER(TO_CHAR(c.dcounter,'HH24')+1)  as "Время",--2
--            c.dcounter as "Дата", --1
--            c.dcounter  as "Время",--2
            NVL(TO_CHAR(trunc(c.M1,6)),'-'), --3
            NVL(TO_CHAR(trunc(c.v1,2)),'-'), -- 4 данные отсутствуют ???? данные есть!
            NVL(TO_CHAR(trunc(c.t1,6)),'-'), -- 5
            NVL(TO_CHAR(trunc(c.P1,6)),'-'), -- 6 11.09.2008 данные есть
            NVL(TO_CHAR(trunc(c.q1,6)),'-'), -- 7 W1 тепловая энергия
            NVL(TO_CHAR(trunc(c.M2,6)),'-'), -- 8
            NVL(TO_CHAR(trunc(V2,2)),'-'),   -- 9
            NVL(TO_CHAR(trunc(c.t2,6)),'-'), -- 10
            NVL(TO_CHAR(trunc(c.P2,6)),'-'), -- 11 11.09.2008 данные есть
            '-', --NVL(TO_CHAR(trunc(c.q2,6)),'-'), -- 12 W2 тепловая энергия
            NVL(TO_CHAR((trunc(c.m1,6)-trunc(c.m2,6))),'-'),-- as "dM12", -- 13 dM12
            '-', --NVL(TO_CHAR((trunc(c.q1,6)-trunc(c.q2,6))),'-'), -- as "dW12", -- 14 dW12
            -- ГВС ---
            NVL(TO_CHAR(trunc(c.m4,2)),'-'), -- 15 M1гвс
            NVL(TO_CHAR(trunc(c.v4,6)),'-'), -- 16 V1гвс
            NVL(TO_CHAR(trunc(c.t4,2)),'-'), -- 17 T1гвс данные отсутствуют!!!
            '-', --NVL(TO_CHAR(trunc(c.p4,2)),'-'), -- 18 P1гвс ????????????
            NVL(TO_CHAR(trunc(c.q2,2)),'-'), -- 19 W1гвс
            '-', --NVL(TO_CHAR(trunc(c.tcool,2)),'-'), -- 20 TCool -> tхв
            '-', --NVL(TO_CHAR(trunc(c.pxb,2)),'-'), -- 21 PXB -> Pxv ??????
--            NVL(TO_CHAR(c.errtime),'-'), -- 22 To
--            NVL(TO_CHAR(c.worktime),'-'), -- 23 Tp
--            NVL(TO_CHAR(60-(60*c.tsum1)),'-'), -- 22 To (Edited 2008.05.16 в минутах)
--            NVL(TO_CHAR(60*c.tsum1),'-'), -- 23 Tp (Edited 2008.05.16 в минутах)
            NVL(TO_CHAR(1-c.tsum1),'-'), -- 22 To (Edited 2008.05.16 в часах)
            NVL(TO_CHAR(c.tsum1),'-'), -- 23 Tp (Edited 2008.05.16 в часах)
            NVL(TO_CHAR(c.errtime),'-'), -- 24 Too
            NVL(TO_CHAR(c.errtime),'-'),  -- 25 Tog
--            SUBSTR(c.hc,1,16) -- для MT200,ТСР показывает коды
            NVL(TO_CHAR(c.hc_code),'-')
/*код схемы СИ-2 Дата, Время,M1,V1,t1,P1,W1,M2,V2,t2,P2,W2,dM,dW,M1гв,V1гв,t1гв,P1гв,W1гв,tхв,Pхв,To,Tp,Too,Tогв,НС */
     from bdevices d, datacurr c, bbuildings b
     where c.id_bd = d.id_bd AND d.id_bu = b.id_bu AND c.id_ptype=3
--     AND b.cshort = 'АТС 750'--'АТС 750'
     AND b.cshort = nodename --'АТС 750'
--     AND c.dcounter>=to_date('10.03.2007 00:00:00','DD.MM.YYYY HH24:MI:SS')
--     AND c.dcounter<=to_date('10.04.2007 23:00:00','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter>=to_date(sdate,'DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate,'DD.MM.YYYY HH24:MI:SS')
     ORDER BY "Дата","Время" desc;
--     ORDER BY "Дата" desc;

 utl_file.put_line(fd,'Дата'||chr(9)||'Время'||chr(9)||'M1'||chr(9)||'V1'||chr(9)||'t1'||chr(9)||'P1'||chr(9)||'W1'||chr(9)||'M2'||chr(9)||'V2'||chr(9)||'t2'||chr(9)||'P2'||chr(9)||'W2'||chr(9)||'dM'||chr(9)||'dW'||chr(9)||'M1гв'||chr(9)||'V1гв'||chr(9)||'t1гв'||chr(9)||'P1гв'||chr(9)||'W1гв'||chr(9)||'tхв'||chr(9)||'Pхв'||chr(9)||'Tо'||chr(9)||'Tр'||chr(9)||'Tоо'||chr(9)||'Tогв'||chr(9)||'НС');
 utl_file.put_line(fd,'дд.мм.гггг'||chr(9)||'ч'||chr(9)||'т'||chr(9)||'м3'||chr(9)||'гр.C'||chr(9)||'кгс/см2'||chr(9)||'Гкал'||chr(9)||'т'||chr(9)||'м3'||chr(9)||'гр.C'||chr(9)||'кгс/см2'||chr(9)||'Гкал'||chr(9)||'т'||chr(9)||'Гкал'||chr(9)||'т'||chr(9)||'м3'||chr(9)||'гр.C'||chr(9)||'кгс/см2'||chr(9)||'Гкал'||chr(9)||'гр.C'||chr(9)||'кгс/см2'||chr(9)||'ч'||chr(9)||'ч'||chr(9)||'ч'||chr(9)||'ч'||chr(9)||'-');
     LOOP
         FETCH cdat INTO
               cdata,chour,cM1,cV1,cT1,cP1,cW1,cM2,cV2,cT2,--10
               cP2,cW2,cdM,cdW,cM1g,cV1g,cT1g,cP1g,cW1g,cTxv,--20
               cPxv,cT_o, cTp,cToo,cTog, /*cHC1,*/cHC2;
         EXIT WHEN cdat%NOTFOUND;
         utl_file.put_line(fd,
                              cdata           ||chr(9)||
                              TO_CHAR(chour)  ||chr(9)||
--                              TO_CHAR(cdata,'DD.MM.YYYY')  ||chr(9)||
--                              TO_CHAR(chour,'HH24')        ||chr(9)||
--                              TO_CHAR(chour)  ||chr(9)||
                              TO_CHAR(cM1)    ||chr(9)||
                              TO_CHAR(cV1)    ||chr(9)||--V1 попадает из M1????????????
                              TO_CHAR(cT1)    ||chr(9)||
                              TO_CHAR(cP1)    ||chr(9)||--P1!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                              TO_CHAR(cW1)    ||chr(9)||
                              TO_CHAR(cM2)    ||chr(9)||
                              TO_CHAR(cV2)    ||chr(9)||--V2
                              TO_CHAR(cT2)    ||chr(9)||
                              TO_CHAR(cP2)    ||chr(9)||--P2
                              TO_CHAR(cW2)    ||chr(9)||
                              TO_CHAR(cdM)    ||chr(9)||
                              TO_CHAR(cdW)    ||chr(9)||--dW
                              TO_CHAR(cM1g)   ||chr(9)||--M1g
                              TO_CHAR(cV1g)   ||chr(9)||--V1g
                              TO_CHAR(cT1g)   ||chr(9)||--t1g
                              TO_CHAR(cP1g)   ||chr(9)||--P1g
                              TO_CHAR(cW1g)   ||chr(9)||--W1g
                              TO_CHAR(cTxv)   ||chr(9)||--txv
                              TO_CHAR(cPxv)   ||chr(9)||--Pxv
                              TO_CHAR(cT_o)   ||chr(9)||--T_o
                              TO_CHAR(cTp)    ||chr(9)||--Tp
                              TO_CHAR(cToo)   ||chr(9)||--Too
                              TO_CHAR(cTog)   ||chr(9)||--Tog
                              /*cHC1 ||'+'||*/cHC2/*'HC'*/
                             );
     END LOOP;
-- utl_file.put_line(fd,'------------------------------------------------------');
-- utl_file.new_line(fd);
 utl_file.put_line(fd,'$$$data_end$$$');
 utl_file.fclose(fd);
close header_1_cur;
CLOSE cdat;

end;
/

