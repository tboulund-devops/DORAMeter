ALTER TABLE branch_types
    MODIFY COLUMN id INT,  -- Specify the column type without AUTO_INCREMENT
    DROP PRIMARY KEY,               -- Remove the primary key constraint temporarily
    ADD PRIMARY KEY (id);  -- Re-add the primary key constraint without AUTO_INCREMENT
