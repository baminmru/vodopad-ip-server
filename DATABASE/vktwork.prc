create or replace procedure VKTWork(pid_bd varchar2,pid_ip varchar2,
                pdcounter varchar2,pptype varchar2,pedizm varchar2,
                ptcool varchar2 default null,pthot varchar2 default null,
                pt1 varchar2 default null,pt2 varchar2 default null,
                pp1 varchar2 default null,pp2 varchar2 default null,
                pm1 varchar2 default null,pm2 varchar2 default null,pdm12 varchar2 default null,
                pdq varchar2 default null,pv1 varchar2 default null,pv2 varchar2 default null,
                ppatm varchar2 default null,pq1 varchar2 default null,pq2 varchar2 default null,
                pnopower varchar2 default null,phc varchar2 default null, 
                pdtc varchar2 default null, phccode varchar2 default null) is

m varchar2(180);
k1 number;
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
      insert into datawork(id,id_bd,id_ptype,dcall,dcounter,tcool,thot,
             t1,t2,p1,p2,m1,m2,dm12,q1,q2,dq,v1,v2,hc,hc_code,dt12)
          values(ToNumber(pid_ip),ToNumber(pid_bd),ToNumber(pptype),sysdate,
            to_date(pdcounter,'yyyymmddhh24miss'),ToNumber(ptcool),ToNumber(pthot),
            ToNumber(pt1),ToNumber(pt2),ToNumber(pp1),ToNumber(pp2),
            ToNumber(pm1),ToNumber(pm2),ToNumber(pdm12),
            q1a,q2a,
            ToNumber(pdq)/k1,ToNumber(pv1),ToNumber(pv2),substr(m,1,180),phccode,
            ToNumber(pt1)-ToNumber(pt2));
    commit;
    Return;
  end if; 
  if pptype = '1' then
      insert into datawork(id,id_bd,id_ptype,dcall,dcounter,tcool,thot,
             t1,t2,patm,p1,p2,m1,m2,dm12,q1,q2,dq,v1,v2,hc,hc_code,datecounter)
          values(ToNumber(pid_ip),ToNumber(pid_bd),ToNumber(pptype),sysdate,
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
      insert into datawork(id,id_bd,id_ptype,dcall,dcounter,
             m1,m2,dm12,q1,q2,dq,v1,v2,errtime)
          values(ToNumber(pid_ip),ToNumber(pid_bd),ToNumber(pptype),sysdate,sysdate,
            ToNumber(pm1),ToNumber(pm2),ToNumber(pdm12),
            ToNumber(pq1)/k1,ToNumber(pq2)/k1,
            ToNumber(pdq)/k1,ToNumber(pv1),ToNumber(pv2),
            ToNumber(pnopower));
    commit;
    Return;
  end if;
end VKTWork;
/

