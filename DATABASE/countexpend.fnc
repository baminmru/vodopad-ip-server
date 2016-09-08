create or replace function CountExpend(texpend varchar2,
                          tparam varchar2, pt varchar2,pp varchar2,pval varchar2) return number is
--texpend : '0' - расходный (т/ч)
--          '1' - объемный

-- tparam : '0'  - л
--          '1'  - м3
--          '2'  - т
t1 number(5,2);
tx number(5,2);
v1 number(14,7);
p1 number(14,7);
px number(14,7);
i integer;
j integer;
dn1 number(10,6);
dn2 number(10,6);
dn3 number(10,6);
dn4 number(10,6);
begin
  if Upper(pval) = 'NULL' then
    Return(null);
  end if;
  v1 := ToNumber(pval);
  if v1 is NULL then
    Return(null);
  end if;
  
  if tparam = '2' then
    Return(v1);
  end if;
  
  px := ToNumber(pp); 
  tx := ToNumber(pt); 
  if px is null then
    Return(null);
  end if;
  if tx is null then
    Return(null);
  end if;
     
  if px < 2 or px > 10 then
    Return(null);
  end if;
  if tx < 0 or tx > 110 then
    Return(null);
  end if;
  
  if tparam = '0' then
    v1 := v1/1000;
    if texpend = '0' then
      v1 := v1 * 60;
    end if;
  end if;
  i := instr(pt,'.');
  if i < 1 then
    i := instr(pt,',');   
  end if;
  if i < 1 then
    i := Length(pt)+1;
  end if;
  
  j := instr(pp,'.');
  if j < 1 then
    j := instr(pp,',');
  end if;
  if j < 1 then
    j := Length(pp)+1;
  end if;
   
  if round(px,0) = px then
    select t1.density,t2.density into dn1,dn2 from tdensity t1,tdensity t2
       where t1.p = px and t1.t = To_Number(substr(pt,1,i-1))
           and t2.p = px and t2.t = To_Number(substr(pt,1,i-1))+1;
    if dn1 is null or dn2 is null then
      Return(null);
    end if;
        
    if Round(tx,0) <> tx then
      dn1 := dn1 + (dn2-dn1)*To_Number('0.'||substr(pt,i+1));
    end if;
  else
    select t1.density,t2.density,t3.density,t4.density into dn1,dn2,dn3,dn4 
       from tdensity t1,tdensity t2,tdensity t3,tdensity t4
       where t1.p = To_Number(substr(pp,1,j-1)) and t1.t = To_Number(substr(pt,1,i-1))
           and t2.p = To_Number(substr(pp,1,j-1)) and t2.t = To_Number(substr(pt,1,i-1))+1
           and t3.p = To_Number(substr(pp,1,j-1))+1 and t3.t = To_Number(substr(pt,1,i-1))
           and t4.p = To_Number(substr(pp,1,j-1))+1 and t4.t = To_Number(substr(pt,1,i-1))+1;
    if dn1 is null or dn2 is null then
      Return(null);
    end if;
    if Round(tx,0) <> tx then
      dn1 := dn1 + (dn2-dn1)*To_Number('0.'||substr(pt,i+1));
      dn3 := dn3 + (dn4-dn3)*To_Number('0.'||substr(pt,i+1));
    end if;
    dn1 := dn1 + (dn3-dn1)*To_Number('0.'||substr(pp,j+1));
  end if;
  Return(v1*(dn1/1000));
  
end CountExpend;
/

