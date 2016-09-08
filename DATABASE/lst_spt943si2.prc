create or replace procedure lst_spt943si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst('��� 252', -- ������� 12.09.2008 ������ �� ���943
                               '��� 316', --������� 19.12.2007
                               '��� 350', --,
                               '��� 360',
                               '��� 368', -- 27.07.09 ����� ��� ���960
                               '��� 446', -- ������� 14.11.2008 ������ ��
                               '��� 543', -- �������� 20.06.2008 ������ �� ��� ���960
                               --'��� 726', -- ������� 31.01.2008 ������ �� ���943
                               '��� 750', -- ������� 12.09.2008 ������ �� ���943
                               '��� 752', -- ������� 28.10.2008 ������ �� ���943
                               '��� 756',
                               '��� 776', -- ������� 12.09.2008 ������ �� ���943
                               '��� 784' -- ������� 28.10.2008 ������ �� ���943
                               --'��� 271'  -- ������� 14.12.2012 
                               );

BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP
                counters.si2spt943(node_name(x),sdate,edate);
              END LOOP;
END;
/

