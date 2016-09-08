create or replace procedure VKTSave(pid_bd varchar2,
                pdcounter varchar2,pptype varchar2,pedizm varchar2,ptcool varchar2,pthot varchar2,
                pt1 varchar2,pt2 varchar2,pp1 varchar2,pp2 varchar2,
                pm1 varchar2,pm2 varchar2,pdm12 varchar2,
                pdq varchar2,pv1 varchar2,pv2 varchar2,
                ppatm varchar2,pq1 varchar2 default null,pq2 varchar2 default null,
                pnopower varchar2, phc varchar2, phccode varchar2, pdtc varchar2) is
m varchar2(180);
m1 varchar2(180);
m2 varchar2(4);
k1 number;
d1 date;
q1a number(14,4);
q2a number(14,4);

begin
  k1 := 1;
  if pedizm = '0' then
    k1 := 4.1868;
  end if;
  if pq1 is null then
    q1a := ToNumber(pt1)*ToNumber(pm1)/1000/k1;
  else
    q1a := ToNumber(pq1);
  end if;
  if pq2 is null then
    q2a := ToNumber(pt2)*ToNumber(pm2)/1000/k1;
  else
    q2a := ToNumber(pq2);
  end if;
  if pptype = '3' then
    m := phc;
    select max(dcounter) into d1 from datacurr where id_bd=pid_bd
        and id_ptype=3 and dcounter=to_date(pdcounter,'yyyymmddhh24miss');
    if d1 is null then
      insert into datacurr(id_bd,id_ptype,dcall,dcounter,tcool,thot,
             t1,t2,p1,p2,m1,m2,dm12,q1,q2,dq,v1,v2,hc,hc_code)
          values(pid_bd,pptype,sysdate,
            to_date(pdcounter,'yyyymmddhh24miss'),ToNumber(ptcool),ToNumber(pthot),
            ToNumber(pt1),ToNumber(pt2),ToNumber(pp1),ToNumber(pp2),
            ToNumber(pm1),ToNumber(pm2),ToNumber(pdm12),
            q1a,q2a,
            ToNumber(pdq)/k1,ToNumber(pv1),ToNumber(pv2),substr(m,1,180),phccode);
    else
      update datacurr set dcall=sysdate,tcool=ToNumber(ptcool),thot=ToNumber(pthot),
          t1=ToNumber(pt1),t2=ToNumber(pt2),dt12=ToNumber(pt1)-ToNumber(pt2),
          p1=ToNumber(pp1),p2=ToNumber(pp2),
          m1=ToNumber(pm1),m2=ToNumber(pm2),dm12=ToNumber(pdm12),
          q1=q1a,q2=q2a,
          dq=ToNumber(pdq)/k1,v1=ToNumber(pv1),
          v2=ToNumber(pv2),hc=substr(m,1,180),hc_code=phccode
        where id_bd=pid_bd
          and id_ptype=3 and dcounter=to_date(pdcounter,'yyyymmddhh24miss');
             
    end if;
    commit;
    --Расчет месячного архива
    VKT24(pid_bd,substr(pdcounter,1,8));
    Return;
  end if; 
  if pptype = '1' then
      insert into datacurr(id_bd,id_ptype,dcall,dcounter,tcool,thot,
             t1,t2,patm,p1,p2,m1,m2,dm12,q1,q2,dq,v1,v2,hc,hc_code,datecounter)
          values(pid_bd,pptype,sysdate,
            sysdate,ToNumber(ptcool),ToNumber(pthot),
            ToNumber(pt1),ToNumber(pt2),ToNumber(ppatm),
            ToNumber(pp1),ToNumber(pp2),
            ToNumber(pm1),ToNumber(pm2),
            ToNumber(pm1)-ToNumber(pm2),
            ToNumber(pq1)/k1,ToNumber(pq2)/k1,
            ToNumber(pdq)/k1,ToNumber(pv1),ToNumber(pv2),substr(m,1,180),phccode,to_date(pdtc,'yyyymmddhh24miss'));
    commit;
    Return;
  end if;
  if pptype = '2' then
      insert into datacurr(id_bd,id_ptype,dcall,dcounter,
             m1,m2,dm12,q1,q2,dq,v1,v2,errtime)
          values(pid_bd,pptype,sysdate,sysdate,
            ToNumber(pm1),ToNumber(pm2),ToNumber(pdm12),
            ToNumber(pq1)/k1,ToNumber(pq2)/k1,
            ToNumber(pdq)/k1,ToNumber(pv1),ToNumber(pv2),
            ToNumber(pnopower));
    commit;
    Return;
  end if;
end VKTSave;
/

