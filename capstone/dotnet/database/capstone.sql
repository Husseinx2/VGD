USE master
GO

--drop database if it exists
IF DB_ID('final_capstone') IS NOT NULL
BEGIN
	ALTER DATABASE final_capstone SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
	DROP DATABASE final_capstone;
END

CREATE DATABASE final_capstone
GO

USE final_capstone
GO

--create tables
CREATE TABLE game (
	game_id int IDENTITY(1,1) NOT NULL,
	title varchar(50) NOT NULL,
	description varchar (MAX) NOT NULL,
	esrb_rating varchar (10) NOT NULL,
	release_date DATE NOT NULL,
	image_url varchar (MAX) NOT NULL,
	trailer_url varchar (MAX) NULL,
	CONSTRAINT PK_games PRIMARY KEY (game_id)
)

CREATE TABLE users (
	user_id int IDENTITY(1,1) NOT NULL,
	username varchar(50) NOT NULL,
	password_hash varchar(200) NOT NULL,
	salt varchar(200) NOT NULL,
	user_role varchar(50) NOT NULL,
	is_deleted bit NOT NULL DEFAULT 0,
	CONSTRAINT PK_user PRIMARY KEY (user_id)
)
CREATE TABLE review (
	review_id int IDENTITY (1, 1) NOT NULL,
    game_id int NOT NULL,
    reviewer_id int NOT NULL,
    review_content varchar (MAX) NOT NULL,
	review_datetime DATE NOT NULL,
    CONSTRAINT [PK_review] PRIMARY KEY (review_id),
    CONSTRAINT [FK_review_game] FOREIGN KEY (game_id) REFERENCES [game] (game_id) ON DELETE CASCADE,
    CONSTRAINT [FK_review_users] FOREIGN KEY (reviewer_id) REFERENCES [users] (user_id),
	CONSTRAINT UQ_UniqueCombination UNIQUE (game_id, reviewer_id)
)
CREATE TABLE comment (
	comment_id int IDENTITY (1, 1) NOT NULL,
    review_id int NOT NULL,
    commenter_id int NOT NULL,
    comment_content varchar (MAX) NOT NULL,
	comment_datetime DATE NOT NULL,
    CONSTRAINT [PK_comment] PRIMARY KEY (comment_id),
    CONSTRAINT [FK_comment_review] FOREIGN KEY (review_id) REFERENCES [review] (review_id) ON DELETE CASCADE,
    CONSTRAINT [FK_comment_users] FOREIGN KEY (commenter_id) REFERENCES [users] (user_id)
)
CREATE TABLE rating (
    game_id int NOT NULL,
    user_id int NOT NULL,
    rating_value int NOT NULL CHECK (rating_value >= 1 AND rating_value <= 5),
    rating_datetime DATE NOT NULL,
    CONSTRAINT [PK_rating] PRIMARY KEY (game_id,user_id),
    CONSTRAINT [FK_rating_game] FOREIGN KEY (game_id) REFERENCES [game] (game_id) ON DELETE CASCADE,
    CONSTRAINT [FK_rating_users] FOREIGN KEY (user_id) REFERENCES [users] (user_id) ON DELETE CASCADE
)
CREATE TABLE  genre (
    genre_id int IDENTITY(1,1) NOT NULL,
	genre_name varchar (200) NOT NULL,
	CONSTRAINT Pk_genre PRIMARY KEY (genre_id)
)
CREATE TABLE  company (
    company_id int IDENTITY(1,1) NOT NULL,
	company_name varchar (200) NOT NULL
    CONSTRAINT Pk_company PRIMARY KEY (company_id)

)

CREATE TABLE  platform (
    platform_id int IDENTITY(1,1) NOT NULL,
	platform_name varchar (200) NOT NULL,
	CONSTRAINT pk_platform PRIMARY KEY (platform_id)

)
CREATE TABLE game_platform(
  platform_id int NOT NULL ,
  game_id int NOT NULL,
  CONSTRAINT [PK_game_platform] PRIMARY KEY (game_id,platform_id),
  CONSTRAINT [FK_game_platform_game] FOREIGN KEY (game_id) REFERENCES [game] (game_id) ON DELETE CASCADE,
  CONSTRAINT [FK_game_platform_platform] FOREIGN KEY (platform_id) REFERENCES [platform] (platform_id)
)

CREATE TABLE game_publisher(
  publisher_id int NOT NULL ,
  game_id int NOT NULL,
  CONSTRAINT [PK_game_publisher] PRIMARY KEY (game_id,publisher_id),
  CONSTRAINT [FK_game_publisher_game] FOREIGN KEY (game_id) REFERENCES [game] (game_id) ON DELETE CASCADE,
  CONSTRAINT [FK_game_publisher_company] FOREIGN KEY (publisher_id) REFERENCES [company] (company_id)
)
CREATE TABLE game_developer(
  developer_id int NOT NULL ,
  game_id int NOT NULL,
  CONSTRAINT [PK_game_developer] PRIMARY KEY (game_id,developer_id),
  CONSTRAINT [FK_game_developer_game] FOREIGN KEY (game_id) REFERENCES [game] (game_id) ON DELETE CASCADE,
  CONSTRAINT [FK_game_developer_company] FOREIGN KEY (developer_id) REFERENCES [company] (company_id)
)



CREATE TABLE game_genre (
  genre_id int NOT NULL ,
  game_id int NOT NULL,
  CONSTRAINT [PK_game_genre] PRIMARY KEY (game_id,genre_id),
  CONSTRAINT [FK_game_genre_game] FOREIGN KEY (game_id) REFERENCES [game] (game_id) ON DELETE CASCADE,
  CONSTRAINT [FK_game_genre_genre] FOREIGN KEY (genre_id) REFERENCES [genre] (genre_id)
)

CREATE TABLE game_list (
game_id int NOT NULL,
user_id int NOT NULL,
game_list_type varchar(50) NOT NULL,
game_list_datetime DATE NOT NULL,
CONSTRAINT [PK_game_list] PRIMARY KEY (game_id, user_id, game_list_type),
CONSTRAINT [FK_game_list_game] FOREIGN KEY (game_id) REFERENCES [game] (game_id) ON DELETE CASCADE,
CONSTRAINT [FK_game_list_users] FOREIGN KEY (user_id) REFERENCES [users] (user_id),
)



--populate default data


--game
INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url) 
VALUES ('Super Mario Bros','Super Mario Bros. is a platform game. In the game, Mario must race through the Mushroom Kingdom and save Princess Toadstool (later Princess Peach) from Bowser. Mario jumps, runs, and walks across each level. The worlds are full of enemies and platforms, and open holes.','E','09/13/1985','https://img-grouvee-com.b-cdn.net/upload/cache/28/d1/28d1a59b8cc9631f5fe0141dc6c06542.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url) 
VALUES ('Grand Theft Auto V','Grand Theft Auto V is an action-adventure game played from either a third-person or first-person perspective. Players complete missions—linear scenarios with set objectives—to progress through the story. Outside of the missions, players may freely roam the open world.','M','09/17/2013', 'https://img-grouvee-com.b-cdn.net/upload/cache/ed/42/ed42cf726c8299b8ee36e0b34754ad61.jpg','https://www.youtube.com/embed/hvoD7ehZPcM');

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url) 
VALUES ('Prey',' In Prey, you awaken aboard Talos I, a space station orbiting the moon in the year 2032. You are the key subject of an experiment meant to alter humanity forever – but things have gone terribly wrong. The space station has been overrun by hostile aliens and you are now being hunted.','M','05/15/2017', 'https://img-grouvee-com.b-cdn.net/upload/cache/cb/f6/cbf6710ef76b9a48bf3f8fe72658d6f5.jpg','https://www.youtube.com/embed/LNHZ9WAertc');

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url) 
VALUES ('Portal','A first-person puzzle game developed by Valve and graduates of DigiPen, Portal forces a human test subject to run a gauntlet of grueling spatial experiments administered by a malfunctioning, psychotic artificial intelligence named GLaDOS.','E','10/10/2007','https://img-grouvee-com.b-cdn.net/upload/cache/e6/4c/e64cf528e1e1716c819fceaff40eb1c6.png', NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Rocket League', 'A unique blend of soccer and vehicular mayhem in an arcade-style format.', 'E', '07/07/15', 'https://img-grouvee-com.b-cdn.net/upload/cache/72/02/72028d6282be01a156a5971e758a9df1.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Animal Crossing: New Horizons', 'A life simulation game where players build 
and customize their own island paradise.', 'E', '03/20/20', 'https://img-grouvee-com.b-cdn.net/upload/cache/8c/8c/8c8c4003b0fc19daa318ecdb32b911e6.png',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Tetris', 'A classic puzzle game where players arrange falling blocks to complete lines and score points.', 'E', '06/06/84', 'https://img-grouvee-com.b-cdn.net/upload/cache/0d/bf/0dbf9d8e753dc7f0249fdb1bad6cfe8b.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Ms. Pac-Man', 'An iconic arcade game where players control Ms. Pac-Man as she navigates mazes, eats pellets, and avoids ghosts.', 'E', '02/03/82', 'https://img-grouvee-com.b-cdn.net/upload/cache/c4/e4/c4e4f60d0b8d288e182488de229c6c00.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Superman 64', 'An infamous game known for its poor controls and gameplay, featuring Superman navigating through rings and completing missions.', 'E', '05/30/99', 'https://img-grouvee-com.b-cdn.net/upload/cache/82/c7/82c7f160789ded78f898bf9f9fe8f6c4.png',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Fortnite', 'A battle royale game where players fight to be the last one standing in a shrinking play area.', 'T', '07/25/17', 'https://img-grouvee-com.b-cdn.net/upload/cache/28/ef/28ef624c95a84ed4a2bd03e60ad8709f.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('E.T. the Extra-Terrestrial', 'An infamous video game adaptation of the movie, known for its poor gameplay and development.', 'E', '12/01/82', 'https://img-grouvee-com.b-cdn.net/upload/cache/7f/1c/7f1c8dd0e64005b7cc838cc4a44a5da7.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Street Fighter II', 'A classic fighting game that introduced iconic characters and special moves, popularizing the fighting game genre.', 'T', '02/06/91', 'https://img-grouvee-com.b-cdn.net/upload/cache/45/4b/454be9f392ebb060c53ce79845980962.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Rainbow Six Siege', 'A tactical shooter that focuses on teamwork and strategy in multiplayer scenarios.', 'M', '12/01/15', 'https://img-grouvee-com.b-cdn.net/upload/cache/71/96/719615abc99060f184f03244f6f83d75.jpg',NULL);

INSERT INTO game (title, description, esrb_rating, release_date, image_url, trailer_url)
VALUES ('Madden NFL 24', 'The latest installment in the Madden NFL series, featuring updated rosters, gameplay improvements, and new features.', 'E', '08/15/23', 'https://img-grouvee-com.b-cdn.net/upload/cache/c8/89/c8895466bb7423fc10d58aeb9a7356ad.jpg','https://www.youtube.com/embed/4o6xDjg2B54');

--users
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('user','Jg45HuwT7PZkfuKTz6IB90CtWY4=','LHxP4Xh7bN0=','user');
INSERT INTO users (username, password_hash, salt, user_role) VALUES ('admin','YhyGVQ+Ch69n4JMBncM4lNF/i9s=', 'Ar/aB2thQTI=','admin');
INSERT users ( username, password_hash, salt, user_role, is_deleted) VALUES ( N'zak', N'R52Qda8Fo6bVPwPI7VLTniQzUfY=', N'bb9nMvnZB0A=', N'user', 0)
INSERT users ( username, password_hash, salt, user_role, is_deleted) VALUES ( N'hussein', N'1h8qPpbdni2MHfA6EfTQgDvZ2SI=', N'i4fH6OEEgiM=', N'user', 0)
--reviews
SET IDENTITY_INSERT [dbo].[review] ON 

INSERT [dbo].[review] ([review_id], [game_id], [reviewer_id], [review_content], [review_datetime]) VALUES (1, 1, 1, N'Really Enjoyed this game', CAST(N'2023-08-09' AS Date))
INSERT [dbo].[review] ([review_id], [game_id], [reviewer_id], [review_content], [review_datetime]) VALUES (2, 1, 2, N'It Was Meh, could be better', CAST(N'2023-08-12' AS Date))
INSERT [dbo].[review] ([review_id], [game_id], [reviewer_id], [review_content], [review_datetime]) VALUES (3, 2, 2, N'Grew up on this really like it', CAST(N'2023-08-10' AS Date))
INSERT [dbo].[review] ([review_id], [game_id], [reviewer_id], [review_content], [review_datetime]) VALUES (4, 14, 2, N'Tried Madden 24, it was said to be "the make it or break it" for EA and I must say, its the same game. Madden 21,22,23,24 are practically all the same, they add a couple of new animations and sell us "new games". The madden franchise needs To start from scratch because at this point I''ll stop buying this illogical game.', CAST(N'2023-08-15' AS Date))
INSERT [dbo].[review] ([review_id], [game_id], [reviewer_id], [review_content], [review_datetime]) VALUES (5, 10, 4, N'This game has been lame since 2019', CAST(N'2023-08-15' AS Date))
INSERT [dbo].[review] ([review_id], [game_id], [reviewer_id], [review_content], [review_datetime]) VALUES (6, 13, 4, N'Kind of hard to get into but fun once you get the hang of it', CAST(N'2023-08-15' AS Date))
SET IDENTITY_INSERT [dbo].[review] OFF
---Comments
SET IDENTITY_INSERT [dbo].[comment] ON 

INSERT [dbo].[comment] ([comment_id], [review_id], [commenter_id], [comment_content], [comment_datetime]) VALUES (1, 1, 2, N'this review sucks go open your eyes', CAST(N'2023-08-09' AS Date))
INSERT [dbo].[comment] ([comment_id], [review_id], [commenter_id], [comment_content], [comment_datetime]) VALUES (2, 2, 1, N'this game is the best what are you talking about?', CAST(N'2023-08-12' AS Date))
INSERT [dbo].[comment] ([comment_id], [review_id], [commenter_id], [comment_content], [comment_datetime]) VALUES (3, 2, 1, N'Maybe if the sped up on the new game I would like it more', CAST(N'2023-08-12' AS Date))
INSERT [dbo].[comment] ([comment_id], [review_id], [commenter_id], [comment_content], [comment_datetime]) VALUES (4, 4, 1, N'I agree, it''s a joke at this point. Don''t get me started on the ratings. the super-star XFactor abilities have been the same forever with generic abilities not designed for the player. Myles Garret was a 99 overall player last year and got 16 sacks but went down 4 overall? This game isn''t serious dude', CAST(N'2023-08-15' AS Date))
INSERT [dbo].[comment] ([comment_id], [review_id], [commenter_id], [comment_content], [comment_datetime]) VALUES (5, 4, 3, N'I live and breathe madden. if madden has 99 enemies i have 99 enemies. if madden has only one fan that''s me and if madden has no fans then im probably dead. don''t talk about madden like you understand it. The developers are working hard and using new latest edge technology to give us realistic gameplay with AI who are smart and know how to block. the blocking system was changed and also 99 new animations and Cornerbacks not giving up easy catches anymore. Open your eyes', CAST(N'2023-08-15' AS Date))
INSERT [dbo].[comment] ([comment_id], [review_id], [commenter_id], [comment_content], [comment_datetime]) VALUES (6, 4, 2, N'Yes so by enhancing the blocking they''re rendering D-Lineman useless. and allowing corners to get beat on go routes. Year after Year this game decides to implement No logic.', CAST(N'2023-08-15' AS Date))
INSERT [dbo].[comment] ([comment_id], [review_id], [commenter_id], [comment_content], [comment_datetime]) VALUES (7, 4, 4, N'Yea 100%, could''ve been way better', CAST(N'2023-08-15' AS Date))
SET IDENTITY_INSERT [dbo].[comment] OFF
--rating
INSERT INTO rating (game_id, user_id, rating_value, rating_datetime) VALUES (1,1,4, '08/09/2023');
INSERT INTO rating (game_id, user_id, rating_value, rating_datetime) VALUES (1,2,3, '08/09/2023');
INSERT INTO rating (game_id, user_id, rating_value, rating_datetime) VALUES (2,1,3, '08/08/2023');
INSERT INTO rating (game_id, user_id, rating_value, rating_datetime) VALUES (3,2,5, '08/08/2023');

--genre 
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Platformer')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Third-Person Shooter')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'First-Person Shooter')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Action')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Adventure')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Puzzle')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Open-World')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Horror')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Sports')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Role-Playing')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Fighting')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'First-Person')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'3D')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Singleplayer')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Sci-fi')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Comedy')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Female Protagonist')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Funny')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Physics')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Story')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Rich')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Classic')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Science')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Atmospheric')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'FPS')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Dark Humor')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Short Adventure')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Driving')
INSERT [dbo].[genre] ( [genre_name]) VALUES (N'Soccer')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Adventure game')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Simulation Game')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Social simulation game')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Strategy')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Action-adventure game')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'battle-royale')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'shooting')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'third-person')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'PvP')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'eSports')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Multiplayer')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Shooter')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Tactical')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Competitive')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Online')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Co-Op')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Hero')
INSERT [dbo].[genre] ( [genre_name]) VALUES (N'Team-Based')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Military War')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Realistic')
INSERT [dbo].[genre] ( [genre_name]) VALUES (N'Destruction')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Difficult')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Simulation')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Football (American)')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'PvP Co-op')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Local Co-Op')
INSERT [dbo].[genre] ( [genre_name]) VALUES ( N'Local Multiplayer')
INSERT [dbo].[genre] ([genre_name]) VALUES ( N'Multiplayer Singleplayer')


INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (1, 1)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (2, 2)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (3, 2)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (4, 2)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (5, 2)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (6, 2)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (7, 2)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (3, 3)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (5, 3)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (1, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (4, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (6, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (12, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (13, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (14, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (15, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (16, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (17, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (18, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (19, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (20, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (21, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (22, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (23, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (24, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (25, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (26, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (27, 4)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (9, 5)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (28, 5)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (29, 5)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (30, 6)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (31, 6)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (32, 6)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (33, 7)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (33, 8)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (34, 9)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (4, 10)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (35, 10)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (36, 10)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (37, 10)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (5, 11)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (11, 12)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (4, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (12, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (13, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (25, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (33, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (38, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (39, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (40, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (41, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (42, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (43, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (44, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (45, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (46, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (47, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (48, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (49, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (50, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (51, 13)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (9, 14)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (33, 14)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (52, 14)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (53, 14)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (54, 14)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (55, 14)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (56, 14)
INSERT [dbo].[game_genre] ([genre_id], [game_id]) VALUES (57, 14)



--company
SET IDENTITY_INSERT [dbo].[company] ON 

INSERT [dbo].[company] ([company_id], [company_name]) VALUES (1, N'Nintendo')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (2, N'Rockstar Games')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (3, N'Arkane Studios')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (4, N'2K')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (5, N'Bungie')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (6, N'Activision')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (7, N'Arc System Works')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (8, N'Artoon')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (9, N'Bethesda')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (10, N'BioWare')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (11, N'Capcom')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (12, N'Epic Games')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (13, N'Ubisoft')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (14, N'Sega')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (15, N'Microsoft')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (16, N'Square Enix')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (17, N'Ark System Works')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (18, N'Valve')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (19, N'Psyonix')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (20, N'Mike Ault')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (21, N'Namco')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (22, N'Titus Interactive')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (23, N'Atari, Inc.')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (24, N'Ubisoft Montreal')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (25, N'Tiburon')
INSERT [dbo].[company] ([company_id], [company_name]) VALUES (26, N'Electronic Arts')
SET IDENTITY_INSERT [dbo].[company] OFF

INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (1, 1)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (2, 2)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (3, 3)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (18, 4)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (19, 5)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (1, 6)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (1, 7)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (21, 8)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (1, 9)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (12, 10)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (23, 11)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (11, 12)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (24, 13)
INSERT [dbo].[game_developer] ([developer_id], [game_id]) VALUES (25, 14)

INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (1, 1)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (2, 2)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (9, 3)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (18, 4)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (20, 5)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (1, 6)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (1, 7)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (15, 7)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (21, 8)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (22, 9)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (12, 10)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (23, 11)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (11, 12)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (13, 13)
INSERT [dbo].[game_publisher] ([publisher_id], [game_id]) VALUES (26, 14)

--Platform

SET IDENTITY_INSERT [dbo].[platform] ON 

INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (1, N'NES')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (2, N'Xbox 360')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (3, N'Xbox one')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (4, N'PlayStation 3')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (5, N'PlayStation 4')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (6, N'PlayStation 5')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (7, N'PC')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (8, N'Nintendo 64')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (9, N'GameCube')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (10, N'Xbox X/S')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (11, N'Playstatopm 4')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (12, N'Nintendo')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (13, N'Mobile')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (14, N'macOS[c]')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (15, N'Windows')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (16, N'iOS[c]')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (17, N'Nintendo Switch')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (18, N'Android[c]')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (19, N'Xbox Series X/S')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (20, N'Atari 2600')
INSERT [dbo].[platform] ([platform_id], [platform_name]) VALUES (21, N'Arcade')
SET IDENTITY_INSERT [dbo].[platform] OFF


INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (1, 1)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (3, 2)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (4, 2)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (5, 2)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (6, 2)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 2)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (10, 2)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (3, 3)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (5, 3)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 3)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 4)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (3, 5)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 5)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (11, 5)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (12, 5)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (12, 6)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 7)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (13, 7)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 8)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (13, 8)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (8, 9)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (3, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (5, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (6, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (14, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (15, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (16, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (17, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (18, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (19, 10)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (20, 11)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (21, 12)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (6, 13)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 13)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (10, 13)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (3, 14)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (5, 14)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (6, 14)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (7, 14)
INSERT [dbo].[game_platform] ([platform_id], [game_id]) VALUES (10, 14)


--game_list


GO