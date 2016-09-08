create or replace procedure lst_MT200si2(sdate IN varchar2, edate IN varchar2)
is
TYPE name_lst IS TABLE OF VARCHAR2(24);
node_name name_lst := name_lst(--'��� 225',
                               --'��� 252', -- ����� 11.09.2008 ������ �� �� ���943
                               '��� 314',--'��� 316', -- ����� 19.12.2007
                               --'��� 360', -- ����� 23.01.2008 ������ �� ���943
                               --'��� 446', -- ����� 16.10.2008 ������ �� �� ���943
                               --'��� 585', -- ����� 28.11.2013 ������ �� �� ���943
                               '��� 714'
                               --'��� 726', -- ����� 24.01.2008 ������ �� ���943
                               --'��� 752', -- ����� 16.10.2008 ������ �� ���943
                               --'��� 757'  -- 2014.10.07 ������ �� �� �� ���943
                               ); -- 10 �����
BEGIN
     FOR x IN
            node_name.FIRST ..
            node_name.LAST
              LOOP -- �������� �� ��������� ��� ��200DS
                counters.si2mt200(node_name(x),sdate,edate);
              END LOOP;
END;
/

