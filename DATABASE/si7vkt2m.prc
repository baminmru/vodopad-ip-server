create or replace procedure si7vkt2m(p IN varchar2,sdate IN varchar2, edate IN varchar2)
AUTHID CURRENT_USER
IS
nodename          varchar2(93);--:= '��� 441';
--nodename = p;
n_dogovor    contract.fld12%type:='-';  --����� �������� �������������� $$$1$$$
kod_scheme   contract.fld109%type:='-'; --��� ����� ��������� $$$2$$$
kod_vich     contract.fld51%type:='-';  --��� ���� ������. $$$3$$$
n_vich       contract.fld12%type:='-';  --����� �������. $$$4$$$
/*----��� ���� ���������������-----------------*/
kod_tipa_preobraz_M1   contract.fld40%type;
kod_tipa_preobraz_M1g   contract.fld41%type;
kod_tipa_preobraz_M2g   contract.fld73%type;
kod_tipa_preobraz_M2   contract.fld104%type;
/*-----�������� �������------------------------*/
us_diam_M1g      contract.fld16%type;
us_diam_M2       contract.fld17%type;
us_diam_M1       contract.fld18%type;
us_diam_M2g      contract.fld95%type;
/*-----���� ��������---------------------------*/
imp_M1      contract.fld96%type;
imp_M2      contract.fld97%type;
imp_M1g      contract.fld98%type;
imp_M2g      contract.fld99%type;
/*-----������ ������� ���������----------------*/
n_gran_M1     contract.fld27%type;
n_gran_M2     contract.fld26%type;
n_gran_M1g     contract.fld25%type;
n_gran_M2g     contract.fld23%type;
/*-----������� �������-------------------------*/
u_gran_M1     contract.fld29%type;
u_gran_M2     contract.fld28%type;
u_gran_M1g     contract.fld21%type;
u_gran_M2g     contract.fld96%type;
/*-------����������� ���������-----------------*/
pogr_M1     contract.fld100%type;
pogr_M2     contract.fld101%type;
pogr_M1g     contract.fld102%type;
pogr_M2g     contract.fld103%type;
/*-----������ ���������� ���������-------------*/
  cursor header_1_cur IS
         select c.fld33,c.fld109,c.fld51,c.fld12, -- ������ ���������
                c.fld40,c.fld41,c.fld73,c.fld104, -- ��� ���� ������������.
                c.fld16,c.fld17,c.fld18,c.fld95,  -- �������� �������
                c.fld96,c.fld97,c.fld98,c.fld99, --���� ��������
                c.fld27,c.fld26,c.fld25,c.fld23, -- ������ ������� ���������
                c.fld29,c.fld28,c.fld21,c.fld96, -- ������� �������
                c.fld100,c.fld101,c.fld102,c.fld103 --����������� ���������
         from bdevices d, contract c, bbuildings b
         where c.id_bd = d.id_bd AND d.id_bu = b.id_bu
--         AND b.cshort = '��� 751';--'��� 750';
         AND b.cshort = nodename;--'��� 750';
/*---������ ���������� �������------------------*/
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
  cW2   varchar2(10);        --9 datacurr.q2%type; -- Q --> W �������� �������
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
            TO_CHAR(c.dcounter, 'DD.MM.YYYY') as "����", --1
            TO_NUMBER(TO_CHAR(c.dcounter,'HH24')+1)  as "�����",--2 ���� �� 1 �� 24
            NVL(TO_CHAR(trunc(c.t1,6)),'-'),         --3
            NVL(TO_CHAR(c.P1),'-'),                  --4 ������ �����������
            NVL(TO_CHAR(trunc(c.M2,6)),'-'),         --5
            '-', --NVL(TO_CHAR(trunc(c.v1,2)),'-'),  --6 ������ �����������
            NVL(TO_CHAR(trunc(c.t2,6)),'-'),         --7
            NVL(TO_CHAR(c.P2),'-'),                  --8 ������ �����������
            NVL(TO_CHAR(trunc(c.dq12,6)),'-'),       --9
            NVL(TO_CHAR(c.errtime),'-'),             --10 To
            NVL(TO_CHAR(c.worktime),'-'),            --11 Tp
            '-', --NVL(TO_CHAR(c.errtime),'-'),      --12 Too
            NVL(TO_CHAR(c.hc_code),'-')
/* ��� ����� ��-7 ����,�����,t1,P1,M2,V2,t2,P2,W2�,To,Tp,Too,�� */
/* ��� ������ ����� �������� ������� ������������� � ���� ������� Q1 � Q2*/

     from bdevices d, datacurr c, bbuildings b
     where c.id_bd = d.id_bd AND d.id_bu = b.id_bu AND c.id_ptype=3
     AND b.cshort = nodename --'����'
--     AND c.dcounter>=to_date('10.03.2007 00:00:00','DD.MM.YYYY HH24:MI:SS')
--     AND c.dcounter<=to_date('10.04.2007 23:00:00','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter>=to_date(sdate||' 00:00:00','DD.MM.YYYY HH24:MI:SS')
     AND c.dcounter<=to_date(edate||' 23:00:00','DD.MM.YYYY HH24:MI:SS')
     ORDER BY "����","�����" desc;
--     ORDER BY "����" desc;
 utl_file.put_line(fd,'����'||chr(9)||'�����'||chr(9)||'t1'||chr(9)||'P1'||chr(9)||'M2'||chr(9)||'V2'||chr(9)||'t2'||chr(9)||'P2'||chr(9)||'W1�'||chr(9)||'T�'||chr(9)||'T�'||chr(9)||'T��'||chr(9)||'��');
 utl_file.put_line(fd,'��.��.����'||chr(9)||'�'||chr(9)||'��.C'||chr(9)||'���/��2'||chr(9)||'�'||chr(9)||'�3'||chr(9)||'��.C'||chr(9)||'���/��2'||chr(9)||'����'||chr(9)||'���'||chr(9)||'���'||chr(9)||'���'||chr(9)||'-');
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

