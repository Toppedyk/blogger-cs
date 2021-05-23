-- CREATE TABLE accounts (
--    id VARCHAR(255) NOT NULL,
--    name VARCHAR(255) NOT NULL,
--    email VARCHAR(255) NOT NULL,
--    picture VARCHAR(255) NOT NULL,
--    PRIMARY KEY (id)
-- );

-- CREATE TABLE blogs (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   title VARCHAR(20) NOT NULL, 
--   body VARCHAR(255), 
--   imgUrl VARCHAR(255),
--   published BOOL,

--   PRIMARY KEY (id),

--   FOREIGN KEY (creatorId)
--   REFERENCES accounts (id)
--   ON DELETE CASCADE
-- );

-- CREATE TABLE comments (
--   id INT NOT NULL AUTO_INCREMENT,
--   creatorId VARCHAR(255) NOT NULL,
--   body VARCHAR(240) NOT NULL, 
--   blog INT NOT NULL,

--   PRIMARY KEY (id), 

--   FOREIGN KEY (creatorId)
--   REFERENCES accounts (id)
--   ON DELETE CASCADE,

--   FOREIGN KEY (blog)
--   REFERENCES blogs (id)
--   ON DELETE CASCADE
-- );