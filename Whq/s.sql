--
-- File generated with SQLiteStudio v3.1.1 on 周六 2月 1 11:17:25 2020
--
-- Text encoding used: System
--
PRAGMA foreign_keys = off;
BEGIN TRANSACTION;

-- Table: SystemLog
CREATE TABLE SystemLog (
    HandleTime     [DATETIME]       NOT NULL,
    operator       [NVARCHAR] (20)  NOT NULL,
    Abstract       [NVARCHAR] (50)  NOT NULL,
    opertorContent [NVARCHAR] (100) NOT NULL
);

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 00:04:48',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 00:04:48登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 00:18:09',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 00:18:09登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:04:15',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:04:15登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:05:34',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:05:34登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:06:58',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:06:58登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:08:19',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:08:19登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:11:07',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:11:07登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:11:35',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:11:35登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:13:21',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:13:21登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:13:50',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:13:50登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-01-31 01:14:24',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-01-31 01:14:24登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-02-01 10:40:16',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-02-01 10:40:16登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-02-01 10:57:27',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-02-01 10:57:27登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-02-01 11:04:54',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-02-01 11:04:54登陆本系统'
                      );

INSERT INTO SystemLog (
                          HandleTime,
                          operator,
                          Abstract,
                          opertorContent
                      )
                      VALUES (
                          '2020-02-01 11:13:17',
                          '袁丁',
                          '登陆系统',
                          '袁丁在2020-02-01 11:13:17登陆本系统'
                      );


COMMIT TRANSACTION;
PRAGMA foreign_keys = on;
