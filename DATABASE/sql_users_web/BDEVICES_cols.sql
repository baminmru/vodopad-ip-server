-- Add/modify columns 
alter table BDEVICES add NPQUERY number(1) default 0;
alter table BDEVICES add NPLOCK date;
-- Add comments to the columns 
comment on column BDEVICES.NPIP
  is 'IP адрес NPORT';
comment on column BDEVICES.NPPASSWORD
  is 'Пароль к NPORT';
comment on column BDEVICES.NPQUERY
  is 'Опрашивать по IP';
comment on column BDEVICES.NPLOCK
  is 'Время до которого устройство считается заблокированным';
