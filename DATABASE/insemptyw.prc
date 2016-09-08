create or replace procedure INSEMPTYW(pid_bd in out varchar2,pdcounter varchar2,
            pid_ptype varchar2,phc VarChar2 default null) is
id_bd1 number;
dt date;
begin
  dt := to_date(pdcounter,'yyyymmddhh24miss');
  select max(id_bd) into id_bd1 from datawork where id_bd+0=pid_bd
        and dcounter=dt and id_ptype=pid_ptype;
  if id_bd1 is null then
    insert into datawork(id_bd,id_ptype,dcall,dcounter,datecounter)
          values(pid_bd,pid_ptype,sysdate,dt,dt);
  end if;

  if pid_ptype = '3' or pid_ptype = '4' then
    update datawork set check_a=0 where id_bd+0=pid_bd
          and dcounter=dt and id_ptype=pid_ptype;
  end if;

--  update plancall set dlastcall=sysdate where id_bd=pid_bd; -- for InsEmpty
--------------------------------------------
-- 060412
--  if pid_ptype = '1' then
--    update datawork set hc=phc where  id_bd+0=pid_bd
--          and dcounter=dt and id_ptype=pid_ptype;
--  end if;
--------------------------------------------
  if id_bd1 is null then
    pid_bd := null;
  else
    pid_bd := id_bd1;
  end if;

  commit;
end INSEMPTYW;
/

