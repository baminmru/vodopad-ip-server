
drop table ANALIZER;
-- Create table
create table ANALIZER
(
  ID_BD          NUMBER(5) not null,
  DLAST          DATE not null,
  LASTLINK       DATE,
  LASTWHOLECHECK DATE,
  HMISSING       NUMBER(5),
  DMISSING       NUMBER(5),
  HCCOUNT        NUMBER(5),
  STATUS         NUMBER(5),
  LINKERRORS     NUMBER(5),
  PERROR         NUMBER(5),
  TERROR         NUMBER(5),
  GERROR         NUMBER(5),
  COLOR          VARCHAR2(20),
  INFO           VARCHAR2(4000)
)
tablespace USERS
  pctfree 10
  initrans 1
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
-- Create/Recreate primary, unique and foreign key constraints 
alter table ANALIZER
  add constraint ANALIZER_PK primary key (ID_BD)
  using index 
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );

drop table ANALIZER_HIST;

create table ANALIZER_HIST
(
  ID_BD          NUMBER(5) not null,
  DLAST          DATE not null,
  LASTLINK       DATE,
  LASTWHOLECHECK DATE,
  HMISSING       NUMBER(5),
  DMISSING       NUMBER(5),
  HCCOUNT        NUMBER(5),
  STATUS         NUMBER(5),
  LINKERRORS     NUMBER(5),
  PERROR         NUMBER(5),
  TERROR         NUMBER(5),
  GERROR         NUMBER(5),
  COLOR          VARCHAR2(20),
  INFO           VARCHAR2(4000)
);

-- Create/Recreate primary, unique and foreign key constraints 
alter table ANALIZER_HIST
  add constraint ANALIZER_HIST_PK primary key (ID_BD,DLAST)
  using index 
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  
drop table ANALIZER_CFG;
  
  
  -- Create table
create table ANALIZER_CFG
(
  ID_BD       NUMBER(5) not null,
  ANALIZENODE NUMBER(1) not null,
  OPENSYSTEM  NUMBER(1),
  T1          NUMBER(1),
  T2          NUMBER(1),
  T3          NUMBER(1),
  T4          NUMBER(1),
  T5          NUMBER(1),
  T6          NUMBER(1),
  V1          NUMBER(1),
  V2          NUMBER(1),
  V3          NUMBER(1),
  V4          NUMBER(1),
  V5          NUMBER(1),
  V6          NUMBER(1),
  M1          NUMBER(1),
  M2          NUMBER(1),
  M3          NUMBER(1),
  M4          NUMBER(1),
  M5          NUMBER(1),
  M6          NUMBER(1),
  P1          NUMBER(1),
  P2          NUMBER(1),
  P3          NUMBER(1),
  P4          NUMBER(1),
  P5          NUMBER(1),
  P6          NUMBER(1),
  G1          NUMBER(1),
  G2          NUMBER(1),
  G3          NUMBER(1),
  G4          NUMBER(1),
  G5          NUMBER(1),
  G6          NUMBER(1),
  Q1          NUMBER(1),
  Q2          NUMBER(1),
  Q3          NUMBER(1),
  Q4          NUMBER(1),
  Q5          NUMBER(1),
  K0          NUMBER(18,6),
  K1          NUMBER(18,6),
  K2          NUMBER(18,6),
  K3          NUMBER(18,6),
  K4          NUMBER(18,6),
  K5          NUMBER(18,6)
);


alter table ANALIZER_CFG
  add constraint ANALIZER_CFG_PK primary key (ID_BD)
  using index 
  tablespace USERS
  pctfree 10
  initrans 2
  maxtrans 255
  storage
  (
    initial 64K
    next 1M
    minextents 1
    maxextents unlimited
  );
  

drop table ANALIZER_MODE;
  
   -- Create table
create table ANALIZER_MODE
(
       MODENAME varchar2(60) not null
);

insert into analizer_cfg ( ID_BD , ANALIZENODE, OPENSYSTEM , T1 , T2 , T3 , T4 , T5 , T6 , V1 , V2 , V3 , V4 , V5 , V6 , M1 , M2 , M3 , M4 , M5 , M6 , P1 , P2 , P3 , P4 , P5 , P6 , G1 , G2 , G3 , G4 , G5 , G6 , Q1 , Q2 , Q3 , Q4 , Q5 , K0 , K1 , K2 , K3 , K4 , K5 )
        select id_bd,  1    ,0                                ,1,2,4,5,0,0                    ,0,0,0,0,0,0                 ,0,0,0,0,0,0              ,1,0,0,0,0,0                  ,0,0,0,0,0,0                  ,0,0,0,0,0              ,1.5,0.25,3,1,1,1
        from bdevices;
        commit;
        
 insert into analizer(
 ID_BD ,DLAST ,LASTLINK ,LASTWHOLECHECK ,HMISSING ,DMISSING ,HCCOUNT ,STATUS ,LINKERRORS ,PERROR ,TERROR ,GERROR ,COLOR ,INFO) 
 select id_bd,sysdate-100,sysdate-100,sysdate-100,0,0,0,0,0,0,0,0,'','' from bdevices;
 commit;
