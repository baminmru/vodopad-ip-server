create or replace force view v_dansh as
select
  ID_BD ID_BD2,
count(*) L_cnt,
to_char(dcounter,'YYYY/MM/DD HH24') DAT,
cast(avg(nvl(tair1,0))as number(18,6)) as L_tair1
,cast(avg(nvl(t1,0)) as number(18,6))  as L_t1
,cast(avg(nvl(t2,0))as number(18,6))   as L_t2
,cast(avg(nvl(t3,0)) as number(18,6))  as L_t3
,cast(avg(nvl(t4,0)) as number(18,6))  as L_t4
,cast(avg(nvl(t5  ,0))as number(18,6)) as L_t5
,cast(avg(nvl(t6  ,0))as number(18,6)) as L_t6
,cast(avg(nvl(G1  ,0))as number(18,6)) as L_G1
,cast(avg(nvl(G2  ,0))as number(18,6)) as L_G2
,cast(avg(nvl(G3  ,0))as number(18,6)) as L_G3
 ,cast(avg(nvl(G4  ,0))as number(18,6)) as L_G4
 ,cast(avg(nvl(G5  ,0))as number(18,6)) as L_G5
 ,cast(avg(nvl(G6  ,0))as number(18,6)) as L_G6
 ,cast(avg(nvl(v1  ,0))as number(18,6)) as L_v1
 ,cast(avg(nvl(v2  ,0))as number(18,6)) as L_v2
 ,cast(avg(nvl(v3  ,0))as number(18,6)) as L_v3
 ,cast(avg(nvl(v4  ,0))as number(18,6)) as L_v4
 ,cast(avg(nvl(v5  ,0))as number(18,6)) as L_v5
 ,cast(avg(nvl(v6  ,0))as number(18,6)) as L_v6
 ,cast(avg(nvl(Q1  ,0))as number(18,6)) as L_Q1
 ,cast(avg(nvl(Q2  ,0))as number(18,6)) as L_Q2
 ,cast(avg(nvl(Q3  ,0))as number(18,6)) as L_Q3
 ,cast(avg(nvl(Q4  ,0))as number(18,6)) as L_Q4
 ,cast(avg(nvl(p1  ,0))as number(18,6)) as L_p1
 ,cast(avg(nvl(p2  ,0))as number(18,6)) as L_p2
 ,cast(avg(nvl(p3  ,0))as number(18,6)) as L_p3
 ,cast(avg(nvl(p4  ,0))as number(18,6)) as L_p4
 ,cast(avg(nvl(p5  ,0))as number(18,6)) as L_p5
  ,cast(avg(nvl(p6  ,0))as number(18,6)) as L_p6
  from datacurr dans
  where id_ptype=1
  group by ID_BD,to_char(dcounter,'YYYY/MM/DD HH24');

