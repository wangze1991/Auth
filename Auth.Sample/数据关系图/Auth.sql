/*==============================================================*/
/* DBMS name:      Microsoft SQL Server 2008                    */
/* Created on:     2016/1/26 15:03:43                           */
/*==============================================================*/


if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Dep_Role') and o.name = 'FK_T_DEP_RO_DEPID_T_DEPART')
alter table T_Dep_Role
   drop constraint FK_T_DEP_RO_DEPID_T_DEPART
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Dep_Role') and o.name = 'FK_T_DEP_RO_ROLEID_T_ROLE')
alter table T_Dep_Role
   drop constraint FK_T_DEP_RO_ROLEID_T_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Dep_User') and o.name = 'FK_T_DEP_US_DEPID_T_DEPART')
alter table T_Dep_User
   drop constraint FK_T_DEP_US_DEPID_T_DEPART
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Dep_User') and o.name = 'FK_T_DEP_US_USERID_T_USER')
alter table T_Dep_User
   drop constraint FK_T_DEP_US_USERID_T_USER
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Module_Button') and o.name = 'FK_T_MODULE_BUTTONID_T_BUTTON')
alter table T_Module_Button
   drop constraint FK_T_MODULE_BUTTONID_T_BUTTON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Module_Button') and o.name = 'FK_T_MODULE_MODULEID_T_MODULE')
alter table T_Module_Button
   drop constraint FK_T_MODULE_MODULEID_T_MODULE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Role_Module_Button') and o.name = 'FK_T_ROLE_M_BUTTONID_T_BUTTON')
alter table T_Role_Module_Button
   drop constraint FK_T_ROLE_M_BUTTONID_T_BUTTON
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Role_Module_Button') and o.name = 'FK_T_ROLE_M_MODULEID_T_MODULE')
alter table T_Role_Module_Button
   drop constraint FK_T_ROLE_M_MODULEID_T_MODULE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Role_Module_Button') and o.name = 'FK_T_ROLE_M_ROLEID_T_ROLE')
alter table T_Role_Module_Button
   drop constraint FK_T_ROLE_M_ROLEID_T_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Role_User') and o.name = 'FK_T_ROLE_U_ROLEID_T_ROLE')
alter table T_Role_User
   drop constraint FK_T_ROLE_U_ROLEID_T_ROLE
go

if exists (select 1
   from sys.sysreferences r join sys.sysobjects o on (o.id = r.constid and o.type = 'F')
   where r.fkeyid = object_id('T_Role_User') and o.name = 'FK_T_ROLE_U_USERID_T_USER')
alter table T_Role_User
   drop constraint FK_T_ROLE_U_USERID_T_USER
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Button')
            and   type = 'U')
   drop table T_Button
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Dep_Role')
            and   type = 'U')
   drop table T_Dep_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Dep_User')
            and   type = 'U')
   drop table T_Dep_User
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Department')
            and   type = 'U')
   drop table T_Department
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Module')
            and   type = 'U')
   drop table T_Module
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Module_Button')
            and   type = 'U')
   drop table T_Module_Button
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Role')
            and   type = 'U')
   drop table T_Role
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Role_Module_Button')
            and   type = 'U')
   drop table T_Role_Module_Button
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_Role_User')
            and   type = 'U')
   drop table T_Role_User
go

if exists (select 1
            from  sysobjects
           where  id = object_id('T_User')
            and   type = 'U')
   drop table T_User
go

/*==============================================================*/
/* Table: T_Button                                              */
/*==============================================================*/
create table T_Button (
   Id                   int                  identity,
   Name                 nvarchar(50)         null,
   Sort                 int                  null,
   Icon                 varchar(50)          null,
   Remak                nvarchar(max)        null,
   CreateTime           datetime             null,
   UpdateUserId         int                  null,
   UpdateTime           datetime             null,
   CreateUserId         int                  null,
   constraint PK_T_BUTTON primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Dep_Role                                            */
/*==============================================================*/
create table T_Dep_Role (
   Id                   int                  not null,
   RoleId               int                  null,
   DepId                varchar(50)          null,
   constraint PK_T_DEP_ROLE primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Dep_User                                            */
/*==============================================================*/
create table T_Dep_User (
   Id                   int                  identity,
   DepId                varchar(50)          null,
   UserId               int                  null,
   constraint PK_T_DEP_USER primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Department                                          */
/*==============================================================*/
create table T_Department (
   Id                   numeric              identity,
   ParentId             varchar(50)          null,
   Sort                 int                  null,
   Name                 nvarchar(50)         null,
   Remark               text                 null,
   CreateUserId         int                  null,
   CreateTime           datetime             null,
   UpdateUserId         int                  null,
   UpdateTime           datetime             null,
   constraint PK_T_DEPARTMENT primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Module                                              */
/*==============================================================*/
create table T_Module (
   Id                   varchar(50)          not null,
   Sort                 int                  null,
   ParentId             varchar(50)          null,
   Description          nvarchar(max)        null,
   Name                 nvarchar(50)         null,
   Url                  varchar(100)         null,
   IsView               bit                  null,
   IsDisabled           bit                  null,
   CreateUserId         int                  null,
   CreateTime           datetime             null,
   UpdateUserId         int                  null,
   UpdateTime           datetime             null,
   constraint PK_T_MODULE primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Module_Button                                       */
/*==============================================================*/
create table T_Module_Button (
   Id                   int                  not null,
   ModuleId             varchar(50)          not null,
   ButtonId             int                  not null,
   constraint PK_T_MODULE_BUTTON primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Role                                                */
/*==============================================================*/
create table T_Role (
   Id                   int                  identity,
   Name                 nvarchar(50)         null,
   Remark               text                 null,
   CreateUserId         int                  null,
   CreateTime           datetime             null,
   UpdateUserId         int                  null,
   UpdateTime           datetime             null,
   constraint PK_T_ROLE primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Role_Module_Button                                  */
/*==============================================================*/
create table T_Role_Module_Button (
   Id                   int                  not null,
   ModuleId             varchar(50)          not null,
   ButtonId             int                  not null,
   RoleId               int                  null,
   constraint PK_T_ROLE_MODULE_BUTTON primary key (Id)
)
go

/*==============================================================*/
/* Table: T_Role_User                                           */
/*==============================================================*/
create table T_Role_User (
   Id                   int                  identity,
   UserId               int                  null,
   RoleId               int                  null,
   constraint PK_T_ROLE_USER primary key (Id)
)
go

/*==============================================================*/
/* Table: T_User                                                */
/*==============================================================*/
create table T_User (
   Id                   int                  identity,
   UserName             nvarchar(50)         null,
   Pwd                  varchar(50)          null,
   Remark               text                 null,
   IsOpen               bit                  null,
   LoginTime            int                  null,
   LastLoginTime        datetime             null,
   CreateUserId         int                  null,
   CreateTime           datetime             null,
   UpdateUser           int                  null,
   UpdateTime           datetime             null,
   constraint PK_T_USER primary key (Id)
)
go

alter table T_Dep_Role
   add constraint FK_T_DEP_RO_DEPID_T_DEPART foreign key (DepId)
      references T_Department (Id)
go

alter table T_Dep_Role
   add constraint FK_T_DEP_RO_ROLEID_T_ROLE foreign key (RoleId)
      references T_Role (Id)
go

alter table T_Dep_User
   add constraint FK_T_DEP_US_DEPID_T_DEPART foreign key (DepId)
      references T_Department (Id)
go

alter table T_Dep_User
   add constraint FK_T_DEP_US_USERID_T_USER foreign key (UserId)
      references T_User (Id)
go

alter table T_Module_Button
   add constraint FK_T_MODULE_BUTTONID_T_BUTTON foreign key (ButtonId)
      references T_Button (Id)
go

alter table T_Module_Button
   add constraint FK_T_MODULE_MODULEID_T_MODULE foreign key (ModuleId)
      references T_Module (Id)
go

alter table T_Role_Module_Button
   add constraint FK_T_ROLE_M_BUTTONID_T_BUTTON foreign key (ButtonId)
      references T_Button (Id)
go

alter table T_Role_Module_Button
   add constraint FK_T_ROLE_M_MODULEID_T_MODULE foreign key (ModuleId)
      references T_Module (Id)
go

alter table T_Role_Module_Button
   add constraint FK_T_ROLE_M_ROLEID_T_ROLE foreign key (RoleId)
      references T_Role (Id)
go

alter table T_Role_User
   add constraint FK_T_ROLE_U_ROLEID_T_ROLE foreign key (RoleId)
      references T_Role (Id)
go

alter table T_Role_User
   add constraint FK_T_ROLE_U_USERID_T_USER foreign key (UserId)
      references T_User (Id)
go

