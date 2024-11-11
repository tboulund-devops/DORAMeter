CREATE TABLE deployments (
    id INT PRIMARY KEY AUTO_INCREMENT,
    branch_id INT NOT NULL,
    deployed_date DATETIME NOT NULL,
    is_failure BIT NOT NULL DEFAULT FALSE,
    FOREIGN KEY (branch_id) REFERENCES branches(id)
);