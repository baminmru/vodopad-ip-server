create or replace procedure auto_lst_full
AUTHID CURRENT_USER
IS
s_date  varchar2(24);   -- дата начальная
e_date  varchar2(24);   -- дата конечная
begin

     s_date := to_char(sysdate-1,'DD.MM.YYYY'); -- '04.01.2008' '10.04.2008';
     e_date := to_char(sysdate-1,'DD.MM.YYYY'); -- '04.01.2008';
     counters.lst_full(s_date,e_date);

end;
/

