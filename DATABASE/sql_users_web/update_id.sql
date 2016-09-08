
declare 
idx number:=1;
begin
while idx<300 loop
begin
update datacurr set dcall=dcall where dcounter < sysdate-10  and id_bd=idx and id_ptype=1;
commit;
dbms_output.put_line( idx||'.1');
update datacurr set dcall=dcall where dcounter < sysdate-10  and id_bd=idx and id_ptype=2;
commit;
dbms_output.put_line( idx||'.2');
update datacurr set dcall=dcall where dcounter < sysdate-10  and id_bd=idx and id_ptype=3;
commit;
dbms_output.put_line( idx||'.3');
update datacurr set dcall=dcall where dcounter < sysdate-10  and id_bd=idx and id_ptype=4;
commit;
dbms_output.put_line( idx||'.4');
 idx:=idx+1;
 end;
end loop;
end;