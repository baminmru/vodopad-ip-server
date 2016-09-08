create or replace procedure check24(p IN varchar2)
AUTHID CURRENT_USER
IS
name_id          varchar2(24); -- := 54; --'АТС 251';
s_hour           number(5);

TYPE hour_typ IS REF CURSOR;
     hour_cur hour_typ;
fd utl_file.file_type;

begin
name_id := p;
fd:= utl_file.fopen('d:\database\utl', 'check24_'||to_char(sysdate-1,'DD-MON')||'.txt', 'w');
 utl_file.put_line(fd,'Результат опроса часовых архивов для ТГК1 за:'||chr(9)||to_char(sysdate-1,'DD.MM.YY'));
 utl_file.put_line(fd,'-------------------------------------------------------------------------');

OPEN hour_cur FOR
     select
       count(*) from datacurr da
       where da.id_bd=to_number(name_id) AND da.id_ptype=3
       AND da.dcounter >= to_date(to_char(sysdate-1,'DD.MM.YY')||' 00:00:00','DD.MM.YY HH24:MI:SS')
       AND da.dcounter <= to_date(to_char(sysdate-1,'DD.MM.YY')||' 23:59:59','DD.MM.YY HH24:MI:SS');

     LOOP
         FETCH hour_cur INTO
               s_hour;
         EXIT WHEN hour_cur%NOTFOUND;
               utl_file.put_line(fd, 'id_bd: '||name_id||chr(9)||to_char(s_hour));
     END LOOP;
 utl_file.put_line(fd,'-------------------------------------------------------------------------');
 utl_file.fclose(fd);
CLOSE hour_cur;

end;
/

