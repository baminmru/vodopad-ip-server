create or replace procedure WriteLog(pid_bd varchar2,ptype varchar2,pdbeg varchar2,
           pdur varchar2,pexam varchar2,presult varchar2,pcport varchar2,
           pnsession varchar2) is
begin
  insert into logcall(id_bd,id_ptype,dbeg,duration,cexamine,cresult,cport,nsession)
    values(pid_bd,ptype,to_date(pdbeg,'yyyymmddhh24miss'),pdur,pexam,
           presult,pcport,pnsession);
  Commit;
end WriteLog;
/

