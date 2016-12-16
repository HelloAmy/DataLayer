SELECT `roleinfo`.`KeyID`,
    `roleinfo`.`RoleName`,
    `roleinfo`.`RoleAlias`,
    `roleinfo`.`IsValid`,
    `roleinfo`.`IsDelete`,
    `roleinfo`.`AddTime`,
    `roleinfo`.`ModifyTime`
FROM `usermanagedb`.`roleinfo`;

SELECT * FROM usermanagedb.roleinfo;

delete from usermanagedb.roleinfo
where keyid=''

insert INTO usermanagedb.roleinfo(KeyID, RoleName,RoleAlias,IsValid)
VALUES('2222222','ADMIN','系统管理员', 1);


show create table usermanagedb.roleinfo;

use usermanagedb;

CREATE TABLE `RoleInfo2` (
   `KeyID` varchar(30) NOT NULL DEFAULT '' COMMENT '主键ID;',
   `RoleName` varchar(100) NOT NULL DEFAULT '' COMMENT '角色名;',
   `RoleAlias` varchar(100) NOT NULL DEFAULT '' COMMENT '角色别名;',
   `IsValid` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否可用;',
   `IsDelete` tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除;',
   `AddTime` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP COMMENT '添加时间;',
   `ModifyTime` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '时间戳;',
   PRIMARY KEY (`KeyID`),
   KEY `RoleInfo_RoleName` (`RoleName`)
 ) ENGINE=InnoDB DEFAULT CHARSET=utf8 COMMENT='角色表' collate utf8_general_cs


use usermanagedb;

/*建立数表UserInfo*/
CREATE TABLE UserInfo
(KeyID varchar(30) NOT NULL DEFAULT '' COMMENT '主键ID;',
LoginName varchar(100) NOT NULL DEFAULT '' COMMENT '登录名;',
LoginPwd varchar(1000) NOT NULL DEFAULT '' COMMENT '密码;',
UserName varchar(200) NOT NULL DEFAULT '' COMMENT '用户名;',
Telephone varchar(20) NOT NULL DEFAULT '' COMMENT '电话;',
Email varchar(100) NOT NULL DEFAULT '' COMMENT '电子邮件;',
IsValid tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否可用;',
IsDelete tinyint(4) NOT NULL DEFAULT '0' COMMENT '是否删除;',
AddTime DateTime NOT NULL DEFAULT now() COMMENT '添加时间;',
ModifyTime timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT '时间戳;',
PRIMARY KEY(KeyID))ENGINE=InnoDB default charset=utf8 COMMENT='用于信息表';


SELECT * FROM UserInfo;