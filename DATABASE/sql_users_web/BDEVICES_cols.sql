-- Add/modify columns 
alter table BDEVICES add NPQUERY number(1) default 0;
alter table BDEVICES add NPLOCK date;
-- Add comments to the columns 
comment on column BDEVICES.NPIP
  is 'IP ����� NPORT';
comment on column BDEVICES.NPPASSWORD
  is '������ � NPORT';
comment on column BDEVICES.NPQUERY
  is '���������� �� IP';
comment on column BDEVICES.NPLOCK
  is '����� �� �������� ���������� ��������� ���������������';
