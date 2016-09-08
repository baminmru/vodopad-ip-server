create or replace procedure SMT2WORK(pid_bd varchar2,pid_ip varchar2,
                pccounter varchar2,pptype varchar2,
                pq1 varchar2,pq2 varchar2,
                pv1 varchar2,pv2 varchar2,pv4 varchar2,pv5 varchar2,pt1 varchar2,
                pt4 varchar2,pt2 varchar2,pt5 varchar2,perrtime varchar2,
                pedizm varchar2,phc varchar2 default null,
                phccode varchar2 default null) is
nq1s datacurr.q1h%type;
nq2s datacurr.q2h%type;
nv1s datacurr.v1h%type;
nv2s datacurr.v2h%type;
nv4s datacurr.v4h%type;
nv5s datacurr.v5h%type;
errs datacurr.errtimeh%type;
--recw datacurr%rowtype;
recw datawork%rowtype;
kq1  number;
kq2  number;
pdcounter date;

d1  date;
pfnd boolean;
id1 number;

begin
  kq1 := 1;
  kq2 := 1;
--  if substr(pedizm,7,1) = '0' then --or substr(pedizm,6,1) = '0' then
--    kq1 := 4186.8;
--  end if;
--  if substr(pedizm,7,1) = '0' then --or substr(pedizm,7,1) = '0' then
--    kq2 := 4186.8;
--  end if;
  pdcounter := to_date(pccounter,'yyyymmddhh24miss');
  select max(id_bd) into id1 from datawork 
    where id_bd=pid_bd and dcounter=pdcounter and id=pid_ip;
  if id1 is null then
    insert into datawork(id,dcounter,dcall,id_bd,id_ptype)
      values(pid_ip,pdcounter,sysdate,pid_bd,pptype);
  end if;
  select max(id_bd) into id1 from datacurr 
    where id_bd=pid_bd and dcounter=pdcounter and id=pid_ip;
  if id1 is null then 
    select * into recw from datawork --несоответствие типа объ€влено как datacurr
--    select * into recw from datacurr -- исправил 16.02.05
      where dcounter=pdcounter and id_ptype=pptype and id_bd+0=pid_bd; 
  else
--    select * into recw from datacurr 
    select * into recw from datawork 
      where dcounter=pdcounter and id_ptype=pptype and id_bd+0=pid_bd; 
  end if;
  
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
  
  pdcounter := to_date(pccounter,'yyyymmddhh24miss');
  if pptype = '3' then
    d1 := pdcounter - 1/24;
  else
    d1 := pdcounter - 1;
  end if; 

  begin
    select  q1h,q2h,v1h,v2h,v4h,v5h,errtimeh 
      into nq1s,nq2s,nv1s,nv2s,nv4s,nv5s,errs from datawork 
         where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
    pfnd := true;
  exception
    when no_data_found then pfnd := false;
  end;

  if pfnd = false then
    begin
      select  q1h,q2h,v1h,v2h,v4h,v5h,errtimeh 
        into nq1s,nq2s,nv1s,nv2s,nv4s,nv5s,errs from datacurr 
           where dcounter=d1 and id_ptype=pptype and id_bd+0=pid_bd;
      pfnd := true;
    exception
      when no_data_found then pfnd := false;
    end;
  end if;
  
  if pfnd then
    recw.q1 := (recw.q1h - nq1s)/kq1; 
    recw.q2 := (recw.q2h - nq2s)/kq2; 
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
  
  update datawork set q1=recw.q1,q2=recw.q2,v1=recw.v1,v2=recw.v2,v4=recw.v4,v5=recw.v5,
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
end SMT2WORK;
/

