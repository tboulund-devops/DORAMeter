CREATE TABLE branches (
    id INT PRIMARY KEY AUTO_INCREMENT,
    name VARCHAR(255) NOT NULL,
    repository_id INT NOT NULL,
    branch_type_id INT NOT NULL,
    first_commit DATETIME NOT NULL,
    closed_date DATETIME,
    deployed_date DATETIME,
    is_failure BIT NOT NULL DEFAULT FALSE,
    FOREIGN KEY (repository_id) REFERENCES repositories(id),
    FOREIGN KEY (branch_type_id) REFERENCES branch_types(id)
);