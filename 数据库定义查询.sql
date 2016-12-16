USE TaxClient_temp2;

SELECT 
    TableName       = case when a.colorder=1 then d.name else '' end,
    TableNameCH     = case when a.colorder=1 then isnull(f.value,'') else '' end,
    FieldIndex   = a.colorder,
    FieldName     = a.name,
    IsIdentity       = case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,
    IsPrimaryKey       = case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=a.id and name in (
                     SELECT name FROM sysindexes WHERE indid in( SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid))) then '√' else '' end,
    DataType       = b.name,
    DataTypeLength       = COLUMNPROPERTY(a.id,a.name,'PRECISION'),
    DigitalLength   = isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0),
    IsNullable     = case when a.isnullable=1 then '√'else '' end,
    DefaultValue     = isnull(e.text,''),
    FieldNameCH   = isnull(g.[value],'')
FROM 
    syscolumns a
left join 
    systypes b 
on 
    a.xusertype=b.xusertype
inner join 
    sysobjects d 
on 
    a.id=d.id  and d.xtype='U' and  d.name<>'dtproperties'
left join 
    syscomments e 
on 
    a.cdefault=e.id
left join 
sys.extended_properties   g 
on 
    a.id=G.major_id and a.colid=g.minor_id  
left join
sys.extended_properties f
on 
    d.id=f.major_id and f.minor_id=0
where 
    d.name='C_Organization'    --如果只查询指定表,加上此红色where条件，tablename是要查询的表名；去除红色where条件查询说有的表信息
order by 
    a.id,a.colorder;

-- 查询所有的表明
SELECT name FROM SYSOBJECTS WHERE TYPE='U';

SELECT 
    TableId=O.[object_id],
    TableName=O.Name,
    IndexId=ISNULL(KC.[object_id],IDX.index_id),
    IndexName=IDX.Name,
    IndexType=KC.type_desc,
    Index_Column_id=IDXC.index_column_id,
    ColumnID=C.Column_id,
    ColumnName=C.Name,
    Sort=CASE INDEXKEY_PROPERTY(IDXC.[object_id],IDXC.index_id,IDXC.index_column_id,'IsDescending')
        WHEN 1 THEN 'DESC' WHEN 0 THEN 'ASC' ELSE '' END,
    PrimaryKey=IDX.is_primary_key,
    [UQIQUE]=IDX.is_unique,
    Ignore_dup_key=IDX.ignore_dup_key,
    Disabled=IDX.is_disabled,
    Fill_factor=IDX.fill_factor,
    Padded=IDX.is_padded
FROM sys.indexes IDX
    INNER JOIN sys.index_columns IDXC
        ON IDX.[object_id]=IDXC.[object_id]
            AND IDX.index_id=IDXC.index_id
    LEFT JOIN sys.key_constraints KC
        ON IDX.[object_id]=KC.[parent_object_id]
            AND IDX.index_id=KC.unique_index_id
    INNER JOIN sys.objects O
        ON O.[object_id]=IDX.[object_id]
    INNER JOIN sys.columns C
        ON O.[object_id]=C.[object_id]
            AND O.type='U'
            AND O.is_ms_shipped=0
            AND IDXC.Column_id=C.Column_id
WHERE IDX.is_primary_key<>1;

