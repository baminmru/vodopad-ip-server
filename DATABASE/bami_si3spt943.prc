create or replace procedure BAMI_SI3SPT943(p IN varchar2, sdate IN varchar2, edate IN varchar2)
AUTHID CURRENT_USER
IS
/* ������ 2014.11.05 ��� ���� ���� 4-� ����� �� ������� ���������� */
nodename          varchar2(24);--:= '����';
--nodename = p;
n_dogovor    contract.fld12%type:='-'; --����� �������� �������������� $$$1$$$
kod_scheme   contract.fld109%type:='-'; --��� ����� ��������� $$$2$$$
kod_vich     contract.fld51%type:='-'; --��� ���� ������. $$$3$$$
n_vich       contract.fld12%type:='-'; --����� �������. $$$4$$$
/*----��� ���� ���������������-----------------*/
kod_tipa_preobraz_M1   contract.fld40%type;  -- ����������
kod_tipa_preobraz_M1g   contract.fld41%type; -- ���������� ���
--kod_tipa_preobraz_M2g   contract.fld73%type; -- ���������� ���� (���������� ��� ��-5 ���-700)
kod_tipa_preobraz_M2   contract.fld104%type; -- ���������� M2
kod_tipa_preobraz_M2g   contract.fld120%type; -- ���������� ���2 (��� ����)
/*-----�������� �������------------------------*/
us_diam_M1g      contract.fld16%type;
us_diam_M2       contract.fld17%type;
us_diam_M1       contract.fld18%type;
--us_diam_M2g      contract.fld95%type; -- ���������� ��� ���������� (��� ���-700 ��-5)
us_diam_M2g      contract.fld117%type;  -- ���������� ���2 (��� ����)
/*-----���� ��������---------------------------*/
imp_M1      contract.fld96%type;
imp_M2      contract.fld97%type;
imp_M1g      contract.fld98%type;
imp_M2g      contract.fld99%type;
/*-----������ ������� ���������----------------*/
n_gran_M1     contract.fld27%type;
n_gran_M2     contract.fld26%type;
n_gran_M1g     contract.fld25%type;
--n_gran_M2g     contract.fld23%type; -- ���������� ��� ���������� (��� ���-700 ��-5)
n_gran_M2g     contract.fld119%type;
/*-----������� �������-------------------------*/
u_gran_M1     contract.fld29%type;
u_gran_M2     contract.fld28%type;
u_gran_M1g     contract.fld21%type;
-- u_gran_M2g     contract.fld96%type; -- ���������� ��� ���������� (��� ���-700 ��-5)
u_gran_M2g     contract.fld118%type;
/*-------����������� ���������-----------------*/
pogr_M1     contract.fld100%type;
pogr_M2     contract.fld101%type;
pogr_M1g     contract.fld102%type;
pogr_M2g     contract.fld103%type;
/*-----������ ���������� ���������-------------*/
  cursor header_1_cur IS
         select c.fld33,c.fld109,c.fld51,c.fld12,  -- ������ ���������
                c.fld40,c.fld41,c.fld104,c.fld120, -- ��� ���� ������������.
                c.fld16,c.fld17,c.fld18,c.fld117,  -- �������� �������
                c.fld96,c.fld97,c.fld98,c.fld99,   -- ���� ��������
                c.fld27,c.fld26,c.fld25,c.fld119,  -- ������ ������� ���������
                c.fld29,c.fld28,c.fld21,c.fld118,  -- ������� �������
                c.fld100,c.fld101,c.fld102,c.fld103 --����������� ���������
         from bdevices d, contract c, bbuildings b
         where c.id_bd = d.id_bd AND d.id_bu = b.id_bu
         AND b.cshort = nodename;--'����';
/*---������ ���������� �������------------------*/
TYPE cdat_typ IS REF CURSOR;
  cdat cdat_typ;
--  cdata datacurr.dcounter%type;
--  chour datacurr.dcounter%type;
  cdata varchar2(12);--1
  chour varchar2(12);--2
  cM1   varchar2(12);--3 datacurr.M1%type;
  cV1   varchar2(12);--4 datacurr.V1%type;
  cT1   varchar2(12);--5 datacurr.T1%type;
  cP1   varchar2(12);--6 datacurr.p1%type;
  cW1   varchar2(12);--7 datacurr.q1%type; -- Q --> W �������� �������
  cM2   varchar2(12);--8 datacurr.m2%type;
  cT2   varchar2(12);--9 datacurr.T2%type;
  cV2   varchar2(12);--10
  cP2   varchar2(12);--11
  cW2   varchar2(12);--12 datacurr.q2%type; -- Q --> W �������� �������
  cdM   varchar2(12);--13 datacurr.dm12%type;
  cdW   varchar2(12);--14 datacurr.dg12%type;
  cM1g  varchar2(12);--15 datacurr.m3%type; --M1��� -> m4
  cV1g  varchar2(12);--16 datacurr.V3%type; --V1��� -> v4
  cT1g  varchar2(12);--17 datacurr.t3%type; -- T3 -> T1��� ????????????????????
  cP1g  varchar2(12);--18 datacurr.p3%type;
  cW1g  varchar2(12);--19 datacurr.q3%type;
  ---/* ����������� ��������� 01.10.2007 */
  cM2g   varchar2(12); --20 M2��
  cV2g   varchar2(12); --21 V2��
  cT2g   varchar2(12); --22 t2��
  cP2g   varchar2(12); --23 P2��
  cW2g   varchar2(12); --24 W2��
  cdMg   varchar2(12); --25 dM��
  cdWg   varchar2(12); --26 dW��
  ---
  cTxv  varchar2(12);--27 datacurr.tcool%type; --TCool -> t�� ??????????
  cPxv  varchar2(12);--28 datacurr.pxb%type; -- PXB -> P�� ?????
  cT_o  varchar2(12);--29
  cTp   varchar2(12);--30
  cToo  varchar2(12);--31
  cTog  varchar2(12);--32
--  cHC1   datacurr.Hc_1%type;
  cHC2   datacurr.Hc_2%type;--33

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
-- fd:= utl_file.fopen('D:\db\utl', nodename||'.txt', 'w'); -- �������� �������
 utl_file.put_line(fd,'����� �������� ��������������:'||chr(9)||'$$$1$$$'||n_dogovor);
 utl_file.put_line(fd,'��� ����� ���������:'||chr(9)||'$$$2$$$'||kod_scheme);
 utl_file.put_line(fd,'��� ���� ����������������:'||chr(9)||'$$$3$$$'||kod_vich);
 utl_file.put_line(fd,'����� ����������������:'||chr(9)||'$$$4$$$'||n_vich);
-- utl_file.new_line(fd);
 utl_file.put_line(fd,'����� ��������� �����(������):'||chr(9)||'$$$11$$$'||'M1'||chr(9)||'V1'||chr(9)||'M2'||chr(9)||'V2'||chr(9)||'M1��'||chr(9)||'V1��'||chr(9)||'M2��'||chr(9)||'V2��');
 utl_file.put_line(fd,'��� ���� ���������������:'||chr(9)||'$$$12$$$'||kod_tipa_preobraz_M1||chr(9)||'-'||chr(9)||kod_tipa_preobraz_M2||chr(9)||'-'||chr(9)||kod_tipa_preobraz_M1g||chr(9)||'-'||chr(9)||kod_tipa_preobraz_M2g||chr(9)||'-');
 utl_file.put_line(fd,'�������� �������, ��:'||chr(9)||'$$$13$$$'||us_diam_M1||chr(9)||'-'||chr(9)||us_diam_M2||chr(9)||'-'||chr(9)||us_diam_M1g||chr(9)||'-'||chr(9)||us_diam_M2g||chr(9)||'-');
 utl_file.put_line(fd,'���� ��������, �/���:'||chr(9)||'$$$14$$$'||imp_M1 ||chr(9)||'-'||chr(9)||imp_M2||chr(9)||'-'||chr(9)||imp_M1g||chr(9)||'-'||chr(9)||imp_M2g||chr(9)||'-');
 utl_file.put_line(fd,'������ ������� ���������, �/�:'||chr(9)||'$$$15$$$'||n_gran_M1||chr(9)||'-'||chr(9)||n_gran_M2||chr(9)||'-'||chr(9)||n_gran_M1g||chr(9)||'-'||chr(9)||n_gran_M2g||chr(9)||'-');
 utl_file.put_line(fd,'������� ������� ���������, �/�:'||chr(9)||'$$$16$$$'||u_gran_M1||chr(9)||'-'||chr(9)||u_gran_M2||chr(9)||'-'||chr(9)||u_gran_M1g||chr(9)||'-'||chr(9)||n_gran_M2g||chr(9)||'-');
 utl_file.put_line(fd,'����������� ����������� ���������, %:'||chr(9)||'$$$17$$$'||pogr_M1||chr(9)||'-'||chr(9)||pogr_M2||chr(9)||'-'||chr(9)||pogr_M1g||chr(9)||'-'||chr(9)||pogr_M2g||chr(9)||'-');
-- utl_file.new_line(fd);
 utl_file.put_line(fd,'$$$data_start$$$');
-- utl_file.new_line(fd);
/* ������� ������*/
OPEN cdat FOR
     select
            TO_CHAR(c.dcounter - 0.00001, 'DD.MM.YYYY') as "����", --1
            TO_NUMBER(TO_CHAR(c.dcounter - 0.00001,'HH24')+1)  as "�����",--2 ���� �� 1 �� 24
            NVL(TO_CHAR(trunc(c.M1,6)),'-'), -- 3
            NVL(TO_CHAR(trunc(c.v1,6)),'-'), -- 4
            NVL(TO_CHAR(trunc(c.t1,6)),'-'), -- 5
            NVL(TO_CHAR(c.P1),'-'),          -- 6
            '-', --NVL(TO_CHAR(trunc(c.q1,6)),'-'), -- 7 W1 (W1 � W2 �� ������������)
            NVL(TO_CHAR(trunc(c.M2,6)),'-'), -- 8
            NVL(TO_CHAR(trunc(V2,6)),'-'),   -- 9
            NVL(TO_CHAR(trunc(c.t2,6)),'-'), -- 10
            NVL(TO_CHAR(trunc(c.P2,6)),'-'), -- 11
            '-', --NVL(TO_CHAR(trunc(c.q2,6)),'-'), -- 12 W2 -> q2 �������� ���������� � ���943
            NVL(TO_CHAR((trunc(c.m1,6)-trunc(c.m2,6))),'-'), -- 13 "dM12", -- 13 dM12
            NVL(TO_CHAR((trunc(c.q1,6)-trunc(c.q2,6))),'-'), -- 14 "dW12", -- 14 dW12
            NVL(TO_CHAR(trunc(c.m4,6)),'-'), -- 15 M1��� -> m4
            NVL(TO_CHAR(trunc(c.v4,6)),'-'), -- 16 V1��� -> v4
            NVL(TO_CHAR(trunc(c.t4,2)),'-'), -- 17 T1��� -> t4
            NVL(TO_CHAR(trunc(c.p3,6)),'-'), -- 18 P1��� -> p3 ������ ���� P4
            '-', --NVL(TO_CHAR(trunc(c.q4,6)),'-'), -- 19 W1��� -> q4
            ---/* ����������� ��������� 01.10.2007 */
            NVL(TO_CHAR(trunc(c.m5,6)),'-'), -- 20 M2��
            NVL(TO_CHAR(trunc(c.v5,6)),'-'), -- 21 V2�� ??
            NVL(TO_CHAR(trunc(c.t5,6)),'-'), -- 22 t2��
            NVL(TO_CHAR(trunc(c.p4,6)),'-'), -- 23 P2�� -> ������ ���� P5
            '-', --NVL(TO_CHAR(trunc(c.q5,6)),'-'), -- 24 W2�� -> q5 �������� ���������� � ���943
            NVL(TO_CHAR(trunc(c.dm45,6)),'-'), -- 25 dM�� ??
            NVL(TO_CHAR(trunc(c.dq45,6)),'-'), -- 26 dW�� ??
            ---
            NVL(TO_CHAR(trunc(c.tcool,6)),'-'), -- 20 (27) TCool -> t��
            NVL(TO_CHAR(trunc(c.pxb,6)),'-'),   -- 21 (28) PXB -> Pxv

            NVL(TO_CHAR(1-c.tsum1),'-'),        -- 22 (29) To
            NVL(TO_CHAR(c.tsum1),'-'),          -- 23 (30) Tp
            '-', --NVL(TO_CHAR(c.errtime),'-'), -- 24 (31) Too
            '-', --NVL(TO_CHAR(c.errtime),'-'), -- 25 (32) Tog
--            NVL(TO_CHAR(c.errtime),'-'),        -- 22 (29) To
--            NVL(TO_CHAR(c.worktime),'-'),       -- 23 (30) Tp
--            NVL(TO_CHAR(c.errtime),'-'),        -- 24 (31) Too
--            NVL(TO_CHAR(c.errtime),'-'),        -- 25 (32) Tog
--            c.hc_1,
--            c.hc_2
--            SUBSTR(c.hc,1,16) -- ��� MT200,��� ���������� ����
            NVL(TO_CHAR(c.hc_code),'-')
--            c.hc -- ��� ���942 ���� �����������
/* ��-3 2014.11.05
����,�����,M1,V1,t1,P1,W1,M2,V2,t2,P2,W2,dM,dW,M1��,V1��,t1��,P1��,W1��,M2��,V2��,t2��,P2��,W2  ��,dM��,dW��,t��,P��,To,Tp,Too,T���,��
*/
     from bdevices d, datacurr c, bbuildings b
     where c.id_bd = d.id_bd AND d.id_bu = b.id_bu AND c.id_ptype=3
     AND b.cshort = nodename --'��� 750'
--     AND c.dcounter>=to_date('10.03.2007 00:00:00','DD.MM.YYYY HH24:MI:SS')
--     AND c.dcounter<=to_date('10.04.2007 23:00:00','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter>=to_date(sdate,'DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate,'DD.MM.YYYY HH24:MI:SS')
         and ( 
    (not c.m1 is null) or 
    (not c.m2 is null) or 
    (not c.m3 is null) or 
    (not c.m4 is null) or 
    (not c.t1 is null) or 
    (not c.t2 is null) or 
    (not c.t3 is null) or 
    (not c.t4 is null) or 
    (not c.v1 is null) or
    (not c.v2 is null) or
    (not c.v3 is null) or
    (not c.v4 is null) 
    )
     ORDER BY "����","�����" desc;
--     ORDER BY "����" desc;
 utl_file.put_line(fd,'����'||chr(9)||'�����'||chr(9)||'M1'||chr(9)||'V1'||chr(9)||'t1'||chr(9)||'P1'||chr(9)||'W1'||chr(9)||'M2'||chr(9)||'V2'||chr(9)||'t2'||chr(9)||'P2'||chr(9)||'W2'||chr(9)||'dM'||chr(9)||'dW'||chr(9)||'M1��'||chr(9)||'V1��'||chr(9)||'t1��'||chr(9)||'P1��'||chr(9)||'W1��'||chr(9)||'M2��'||chr(9)||'V2��'||chr(9)||'t2��'||chr(9)||'P2��'||chr(9)||'W2��'||chr(9)||'dM��'||chr(9)||'dW��'||chr(9)||'t��'||chr(9)||'P��'||chr(9)||'T�'||chr(9)||'T�'||chr(9)||'T��'||chr(9)||'T���'||chr(9)||'��');
 utl_file.put_line(fd,'��.��.����'||chr(9)||'�'||chr(9)||'�'||chr(9)||'�3'||chr(9)||'��.C'||chr(9)||'���/��2'||chr(9)||'����'||chr(9)||'�'||chr(9)||'�3'||chr(9)||'��.C'||chr(9)||'���/��2'||chr(9)||'����'||chr(9)||'�'||chr(9)||'����'||chr(9)||'�'||chr(9)||'�3'||chr(9)||'��.C'||chr(9)||'���/��2'||chr(9)||'����'||chr(9)||'�'||chr(9)||'�3'||chr(9)||'��.C'||chr(9)||'���/��2'||chr(9)||'����'||chr(9)||'�'||chr(9)||'����'||chr(9)||'��.C'||chr(9)||'���/��2'||chr(9)||'�'||chr(9)||'�'||chr(9)||'�'||chr(9)||'�'||chr(9)||'-');
     LOOP
         FETCH cdat INTO
               cdata,chour,cM1,cV1,cT1,cP1,cW1,cM2,cV2,cT2,/*10*/
               cP2,cW2,cdM,cdW,cM1g,cV1g,cT1g,cP1g,cW1g,/*19*/
               cM2g,cV2g,cT2g,cP2g,cW2g,cdMg,cdWg,/*26*/
               cTxv,cPxv,cT_o, cTp,cToo,cTog, /*cHC1,*/cHC2; -- 33
         EXIT WHEN cdat%NOTFOUND;
         utl_file.put_line(fd,
                              cdata           ||chr(9)||--1
                              TO_CHAR(chour)  ||chr(9)||--2
                              TO_CHAR(cM1)    ||chr(9)||--3
                              TO_CHAR(cV1)    ||chr(9)||--4 V1
                              TO_CHAR(cT1)    ||chr(9)||--5
                              TO_CHAR(cP1)    ||chr(9)||--6 P1
                              TO_CHAR(cW1)    ||chr(9)||--7
                              TO_CHAR(cM2)    ||chr(9)||--8
                              TO_CHAR(cV2)    ||chr(9)||--9  V2
                              TO_CHAR(cT2)    ||chr(9)||--10 T2
                              TO_CHAR(cP2)    ||chr(9)||--11 P2
                              TO_CHAR(cW2)    ||chr(9)||--12 W2 -> q2
                              TO_CHAR(cdM)    ||chr(9)||--13 dM12
                              TO_CHAR(cdW)    ||chr(9)||--14 dW12
                              TO_CHAR(cM1g)   ||chr(9)||--15 M1g -> m4
                              TO_CHAR(cV1g)   ||chr(9)||--16 V1g -> m5
                              TO_CHAR(cT1g)   ||chr(9)||--17 t1g -> t4
                              TO_CHAR(cP1g)   ||chr(9)||--18 P1g -> p4
                              TO_CHAR(cW1g)   ||chr(9)||--19 W1g -> q4
                              --
                              /*M2��,V2��,t2��,P2��,W2��,dM��,dW��*/
                              TO_CHAR(cM2g)   ||chr(9)||--20 M2��
                              TO_CHAR(cV2g)   ||chr(9)||--21 V2��
                              TO_CHAR(cT2g)   ||chr(9)||--22 t2��
                              TO_CHAR(cP2g)   ||chr(9)||--23 P2��
                              TO_CHAR(cW2g)   ||chr(9)||--24 W2�� -> q5
                              TO_CHAR(cdMg)   ||chr(9)||--25 dM��
                              TO_CHAR(cdWg)   ||chr(9)||--26 dW��
                              --
                              TO_CHAR(cTxv)   ||chr(9)||--27 txv
                              TO_CHAR(cPxv)   ||chr(9)||--28Pxv
                              TO_CHAR(cT_o)   ||chr(9)||--29 T_o
                              TO_CHAR(cTp)    ||chr(9)||--30 Tp
                              TO_CHAR(cToo)   ||chr(9)||--31 Too
                              TO_CHAR(cTog)   ||chr(9)||--32 Tog
                              /*cHC1 ||'+'||*/cHC2/*'HC'*/ --33
                             );
     END LOOP;
-- utl_file.put_line(fd,'------------------------------------------------------');
-- utl_file.new_line(fd);
 utl_file.put_line(fd,'$$$data_end$$$');
 utl_file.fclose(fd);
close header_1_cur;
CLOSE cdat;

end;

--end SI3SPT943;
/

