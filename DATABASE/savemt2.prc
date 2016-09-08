create or replace procedure savemt2(pid_bd varchar2,pccounter varchar2,pptype varchar2,
                pq1 varchar2,pq2 varchar2,
                pv1 varchar2,pv2 varchar2,pv4 varchar2,pv5 varchar2,pt1 varchar2,
                pt4 varchar2,pt2 varchar2,pt5 varchar2,perrtime varchar2,
                pedizm varchar2,pclock varchar2,phc varchar2 default null,phccode varchar2 default null) is

nq1s datacurr.q1h%type;
nq2s datacurr.q2h%type;
nv1s datacurr.v1h%type;
nv2s datacurr.v2h%type;
nv4s datacurr.v4h%type;
nv5s datacurr.v5h%type;
errs datacurr.errtimeh%type;
recw datacurr%rowtype;
pdcounter date;
d1  date;
pfnd boolean;

begin
  
  -- выборка записи за текущую дату
  pdcounter := to_date(pccounter,'yyyymmddhh24miss');  

  begin
    select * into recw from datacurr 
      where dcounter=pdcounter and id_ptype=pptype and id_bd+0=pid_bd;      
  exception
    when others then Return;
  end;

  -- замена значений на вновь заданные
  recw.q1h := NVL(ToNumber(pq1),recw.q1h);
  recw.q2h := NVL(ToNumber(pq2),recw.q2h);
  
  recw.errtimeh := NVL(ToNumber(perrtime),recw.errtimeh);
  
  recw.v1h := Nvl(ToNumber(pv1),recw.v1h);
  recw.v2h := Nvl(ToNumber(pv2),recw.v2h);
  recw.v4h := Nvl(ToNumber(pv4),recw.v4h);
  recw.v5h := Nvl(ToNumber(pv5),recw.v5h);
  
  recw.t1 := Nvl(ToNumber(pt1),recw.t1);
  recw.t2 := Nvl(ToNumber(pt2),recw.t2);
  recw.t4 := Nvl(ToNumber(pt4),recw.t4);
  recw.t5 := Nvl(ToNumber(pt5),recw.t5);
  
  -- выборка записи за предыдущую дату
  if pptype = '3' then
    d1 := pdcounter - 1/24;
  else
    d1 := pdcounter - 1;
  end if; 

  begin
    select  q1h,q2h,v1h,v2h,v4h,v5h,errtimeh 
      into nq1s,nq2s,nv1s,nv2s,nv4s,nv5s,errs from datacurr 
         where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
    pfnd := true;
  exception
    when no_data_found then pfnd := false;
  end;

  -- вычисление разницы между значениями за текущую и предыдущую дату
  if pfnd then
    recw.q1 := (recw.q1h - nq1s);
    recw.q2 := (recw.q2h - nq2s);
    recw.errtime := recw.errtimeh - errs;
    
    recw.m1 := recw.v1h - nv1s;
    recw.m2 := recw.v2h - nv2s;
    recw.m4 := recw.v4h - nv4s;
    recw.m5 := recw.v5h - nv5s;
  end if;
  
  recw.v1 := recw.m1;
  recw.v2 := recw.m2;
  recw.v4 := recw.m4;
  recw.v5 := recw.m5;
  
  -- запись обновленных значений за текущую дату
  update datacurr set q1=recw.q1,q2=recw.q2,v1=recw.v1,v2=recw.v2,v4=recw.v4,v5=recw.v5,
         m1=recw.m1,m2=recw.m2,m4=recw.m4,m5=recw.m5,
         q1h=recw.q1h,q2h=recw.q2h,
         v1h=recw.v1h,v2h=recw.v2h,
         v4h=recw.v4h,v5h=recw.v5h,
         dt12=recw.t1-recw.t2,dcall=sysdate,dt45=recw.t4-recw.t5,
         dv12=recw.v1-recw.v2,dv45=recw.v4-recw.v5,
         dm12=recw.m1-recw.m2,dm45=recw.m4-recw.m5,
         t1=recw.t1,t2=recw.t2,t4=recw.t4,t5=recw.t5,
         errtimeh=recw.errtimeh,errtime=recw.errtime,
         unitsr=pedizm,hc=phc,hc_code=phccode
    where dcounter=pdcounter and id_ptype=pptype and id_bd+0=pid_bd;      
  commit;

  -- выборка записи за следующую дату
  if pptype = '3' then
    d1 := pdcounter + 1/24;
  else
    d1 := pdcounter + 1;
  end if; 
  
  begin
    select  q1h,q2h,v1h,v2h,v4h,v5h,errtimeh 
      into nq1s,nq2s,nv1s,nv2s,nv4s,nv5s,errs from datacurr 
         where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
  exception
    -- выход, если нет записи
    when no_data_found then Return;
  end;
  
  -- вычисление разницы между значениями за следующую и текущую дату
  nq1s := nq1s - recw.q1h;
  nq2s := nq2s - recw.q2h;
  nv1s := nv1s - recw.v1h;
  nv2s := nv2s - recw.v2h;
  nv4s := nv4s - recw.v4h;
  nv5s := nv5s - recw.v5h;
  errs := errs - recw.errtimeh;
  
  -- запись обновленных значений за следующую дату
  update datacurr set q1=nq1s,q2=nq2s,v1=nv1s,v2=nv2s,v4=nv4s,v5=nv5s,
         m1=nv1s,m2=nv2s,m4=nv4s,m5=nv5s,
         dm12=nv1s-nv2s,dm45=nv4s-nv5s,
         dv12=nv1s-nv2s,dv45=nv4s-nv5s,
         errtime=errs,hc=phc,hc_code=phccode
    where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
  Commit;
  
end savemt2;
/

