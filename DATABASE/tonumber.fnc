create or replace function ToNumber(sin varchar2) return number is
  Result number;
  m varchar2(12);
  n number;
  l integer;
begin
  Result := 0;
  if Upper(sin) = 'NULL' then
    Return(null);
  end if;
  if rTrim(sin) is null then
    Return(null);
  end if;
  n := instr(sin,',');
  if n < 1 then
    n := instr(sin,'.');
  end if;
  if n < 1 then
      Return(To_Number(sin));
  end if;
  if n > 1 then
    Result := To_Number(substr(sin,1,n-1));
  end if;
  if n = Length(sin) then
    Return(Result);
  end if;
  m := substr(sin,n+1);
  l := Length(m);
  n := ToNumber(m);
  loop
    n := n/10;
    l := l - 1;
    exit when l=0;
  end loop;
  if Result < 0 or instr(sin,'-') = 1 then  --van 01.11.04 --a0211
    Return(Result-n);
  else
    Return(Result+n);
  end if;
exception
  when others then Return(null);
end ToNumber;
/

