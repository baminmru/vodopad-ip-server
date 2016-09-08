CREATE OR REPLACE TRIGGER "DATACURR_NOW"
  before insert or update on datacurr
  for each row
declare
  id_dev1 number(2);
begin
-- edit by bami  16.06.2009
  if :new.datecounter is null then 
     :new.datecounter := :new.dcounter;
  end if;
  if :new.id is null then 
     :new.id := datacurr_seq.nextval;
  end if;
-- end  edit  

-- edit by bami  21.02.2013
   -- convertion to MDJ to GKAL 
   :new.dans1 := :new.q1/ 4.1868/1000;
   :new.dans2 := :new.q2/ 4.1868/1000;
   :new.dans3 := :new.q3/ 4.1868/1000;
   :new.dans4 := :new.q4/ 4.1868/1000;
   :new.dans5 := :new.q5/ 4.1868/1000;
 
  
-- end  edit  


-- 2010.12.08 id_dev = 12; VKT7
  if :new.id_ptype <> 1 then
    select id_dev into id_dev1 from bdevices where id_bd=:new.id_bd;
    -- if (id_dev1 <> 12) and (id_dev1 <> 7) and (id_dev1 <> 3) and (id_dev1 <> 2) then
    --  Return;
    -- end if;

    if (:new.dq12 is null) and (:new.q1 is not null) and (:new.q2 is not null) then
      :new.dq12 := :new.q1 - :new.q2;
    end if;
    if (:new.dq45 is null) and (:new.q4 is not null) and (:new.q5 is not null) then
      :new.dq45 := :new.q4 - :new.q5;
    end if;

    if (:new.dv12 is null) and (:new.v1 is not null) and (:new.v2 is not null) then
      :new.dv12 := :new.v1 - :new.v2;
    end if;
    if (:new.dv45 is null) and (:new.v4 is not null) and (:new.v5 is not null) then
      :new.dv45 := :new.v4 - :new.v5;
    end if;

  end if;
  --select id_dev into id_dev1 from bdevices where id_bd=:new.id_bd;
  if (:new.dt12 is null) and (:new.t1 is not null) and (:new.t2 is not null) then
    :new.dt12 := :new.t1 - :new.t2;
  end if;
  if (:new.dt45 is null) and (:new.t4 is not null) and (:new.t5 is not null) then
    :new.dt45 := :new.t4 - :new.t5;
  end if;

--  if id_dev1 = 3 and nvl(:new.t1,0) > 0 and nvl(:new.t2,0) > 0 then
--    :new.dt12 := :new.t1 - :new.t2;
--  end if;

  if (:new.dp12 is null) and (:new.p1 is not null) and (:new.p2 is not null) then
    :new.dp12 := :new.p1 - :new.p2;
  end if;
  if (:new.dp45 is null) and (:new.p4 is not null) and (:new.p5 is not null) then
    :new.dp45 := :new.p4 - :new.p5;
  end if;

  if (:new.dg12 is null) and (:new.g1 is not null) and (:new.g2 is not null) then
    :new.dg12 := :new.g1 - :new.g2;
  end if;
  if (:new.dg45 is null) and (:new.g4 is not null) and (:new.g5 is not null) then
    :new.dg45 := :new.g4 - :new.g5;
  end if;

  if (:new.dm12 is null) and (:new.m1 is not null) and (:new.m2 is not null) then
    :new.dm12 := :new.m1 - :new.m2;
  end if;
  if (:new.dm45 is null) and (:new.m4 is not null) and (:new.m5 is not null) then
    :new.dm45 := :new.m4 - :new.m5;
  end if;

end Datacurr_now;
/

