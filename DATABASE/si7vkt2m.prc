create or replace procedure si7vkt2m(p IN varchar2,sdate IN varchar2, edate IN varchar2)
AUTHID CURRENT_USER
IS
nodename          varchar2(93);--:= 'АТС 441';
--nodename = p;
n_dogovor    contract.fld12%type:='-';  --номер договора теплоснабжения $$$1$$$
kod_scheme   contract.fld109%type:='-'; --код схемы измерения $$$2$$$
kod_vich     contract.fld51%type:='-';  --код типа вычисл. $$$3$$$
n_vich       contract.fld12%type:='-';  --номер теловыч. $$$4$$$
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
         select c.fld33,c.fld109,c.fld51,c.fld12, -- первый заголовок
                c.fld40,c.fld41,c.fld73,c.fld104, -- код типа преобразоват.
                c.fld16,c.fld17,c.fld18,c.fld95,  -- условный диаметр
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
  cdata varchar2(12);        --1
  chour varchar2(12);        --2
  cT1   varchar2(10);        --3 datacurr.T1%type;
  cP1   varchar2(10);        --4
  cM2   varchar2(10);        --5 datacurr.m2%type;
  cV2   varchar2(10);        --6
  cT2   varchar2(10);        --7 datacurr.T2%type;
  cP2   varchar2(10);        --8 
  cW2   varchar2(10);        --9 datacurr.q2%type; -- Q --> W тепловая энергии
  cT_o  varchar2(10);        --10
  cTp   varchar2(10);        --11
  cToo  varchar2(10);        --12
  cHC1   datacurr.Hc_1%type; --13

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
            TO_NUMBER(TO_CHAR(c.dcounter,'HH24')+1)  as "Время",--2 Часы от 1 до 24
            NVL(TO_CHAR(trunc(c.t1,6)),'-'),         --3
            NVL(TO_CHAR(c.P1),'-'),                  --4 данные отсутствуют
            NVL(TO_CHAR(trunc(c.M2,6)),'-'),         --5
            '-', --NVL(TO_CHAR(trunc(c.v1,2)),'-'),  --6 данные отсутствуют
            NVL(TO_CHAR(trunc(c.t2,6)),'-'),         --7
            NVL(TO_CHAR(c.P2),'-'),                  --8 данные отсутствуют
            NVL(TO_CHAR(trunc(c.dq12,6)),'-'),       --9
            NVL(TO_CHAR(c.errtime),'-'),             --10 To
            NVL(TO_CHAR(c.worktime),'-'),            --11 Tp
            '-', --NVL(TO_CHAR(c.errtime),'-'),      --12 Too
            NVL(TO_CHAR(c.hc_code),'-')
/* код схемы СИ-7 Дата,Время,t1,P1,M2,V2,t2,P2,W2з,To,Tp,Too,НС */
/* для данной схемы тепловая энергия представленна в виде разницы Q1 и Q2*/

     from bdevices d, datacurr c, bbuildings b
     where c.id_bd = d.id_bd AND d.id_bu = b.id_bu AND c.id_ptype=3
     AND b.cshort = nodename --'СММТ'
--     AND c.dcounter>=to_date('10.03.2007 00:00:00','DD.MM.YYYY HH24:MI:SS')
--     AND c.dcounter<=to_date('10.04.2007 23:00:00','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter>=to_date(sdate||' 00:00:00','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:00:00','DD.MM.YYYY HH24:MI:SS')
     ORDER BY "Дата","Время" desc;
--     ORDER BY "Дата" desc;
 utl_file.put_line(fd,'Дата'||chr(9)||'Время'||chr(9)||'t1'||chr(9)||'P1'||chr(9)||'M2'||chr(9)||'V2'||chr(9)||'t2'||chr(9)||'P2'||chr(9)||'W1з'||chr(9)||'Tо'||chr(9)||'Tр'||chr(9)||'Tоо'||chr(9)||'НС');
 utl_file.put_line(fd,'дд.мм.гггг'||chr(9)||'ч'||chr(9)||'гр.C'||chr(9)||'кгс/см2'||chr(9)||'т'||chr(9)||'м3'||chr(9)||'гр.C'||chr(9)||'кгс/см2'||chr(9)||'Гкал'||chr(9)||'мин'||chr(9)||'мин'||chr(9)||'мин'||chr(9)||'-');
     LOOP
         FETCH cdat INTO
               cdata,chour,cT1,cP1,cM2,cV2,--6
               cT2,cP2,cW2,                --9
               cT_o,cTp,cToo,cHC1;         --13
         EXIT WHEN cdat%NOTFOUND;
         utl_file.put_line(fd,
                              cdata           ||chr(9)|| --1
                              TO_CHAR(chour)  ||chr(9)|| --2
                              TO_CHAR(cT1)    ||chr(9)|| --3
                              TO_CHAR(cP1)    ||chr(9)|| --4
                              TO_CHAR(cM2)    ||chr(9)|| --5
                              TO_CHAR(cV2)    ||chr(9)|| --6
                              TO_CHAR(cT2)    ||chr(9)|| --7
                              TO_CHAR(cP2)    ||chr(9)|| --8
                              TO_CHAR(cW2)    ||chr(9)|| --9
                              TO_CHAR(cT_o)   ||chr(9)|| --10
                              TO_CHAR(cTp)    ||chr(9)|| --11
                              TO_CHAR(cToo)   ||chr(9)|| --12
                              cHC1/*'HC'*/ --13
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

