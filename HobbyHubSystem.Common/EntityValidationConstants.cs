﻿namespace HobbyHubSystem.Common
{
    public static class EntityValidationConstants
    {
        public static class HobbyUser
        {
            public const int UserNameMax = 15;
            public const int UserNameMin = 3;

            public const int FirstNameMax = 25;
            public const int FirstNameMin = 2;

            public const int LastNameMax = 25;
            public const int LastNameMin = 2;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class Category
        {
            public const int NameMax = 50;
            public const int NameMin = 3;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class Hobby
        {
            public const int NameMax = 30;
            public const int NameMin = 2;

            public const int DescriptionMax = 5000;
            public const int DescriptionMin = 3;

            public const int ImageUrlMaxLength = 2048;
        }

        public static class Hub
        {
            public const int NameMax = 30;
            public const int NameMin = 2;

            public const int AboutMax = 5000;
            public const int AboutMin = 3;
        }

        public static class Question
        {
            
            public const int ContentMax = 10000;
            public const int ContentMin = 2;
        }

        public static class Answer
        {
            public const int DescriptionMax = 10000;
            public const int DescriptionMin = 2;
        }

        public static class DiscussionTopic
        {
            public const int TitleMax = 30;
            public const int TileMin = 2;
        }

        public static class Article
        {
            public const int TitleMax = 30;
            public const int TileMin = 2;

            public const int ContentMax = 50000;
            public const int ContentMin = 3;
        }

        public class Event
        {
            public const int TitleMax = 30;
            public const int TileMin = 2;

            public const int DescriptionMax = 50000;
            public const int DescriptionMin = 3;

            public const int LocationMax = 30;
            public const int LocationMin = 3;
        }
    }
}
