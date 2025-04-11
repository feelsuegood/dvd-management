# DVD Management System

## Overview

This project is DVD movie management system for community library. It is console application that staff and members can register, borrow, return and manage DVD. System have simple interface that both staff and library user can use easily.

## Key Features

### Staff Functions

- **DVD Management**: Add new DVDs to the system, update existing entries, and remove obsolete titles
- **Member Management**: Register new members, update member information, and remove inactive accounts
- **Contact Lookup**: Find member contact information by searching with their name
- **Movie Tracking**: View a list of members currently borrowing a specific movie
- **System Administration**: Manage all aspects of the DVD library system

### Member Functions

- **Account Access**: Secure login/logout functionality
- **Movie Operations**: Borrow and return movies with simple processes
- **Personal Dashboard**: View currently borrowed movies and borrowing history
- **Movie Discovery**: Search and browse available movies by title, genre, or classification

## Technology Stack

- **Language**: C# (.NET 7.0)
- **Framework**: .NET Console Application
- **Development Environment**: Visual Studio
- **Data Storage**: Text file system

## Project Structure

```
DVDManagement/
├── DVDManagement.sln                   # Solution file
├── DVDManagement/
│   ├── Program.cs                      # Main program file
│   ├── Movie.cs                        # Movie class file
│   ├── Member.cs                       # Member class file
│   ├── MovieCollection.cs              # MovieCollection class file
│   ├── MemberCollection.cs             # MemberCollection class file
│   ├── StaffMenu.cs                    # Staff interface implementation
│   ├── MemberMenu.cs                   # Member interface implementation
│   ├── iMovieCollection.cs             # Interface for movie collections
│   ├── movies.txt                      # Movie data storage
│   └── members.txt                     # Member data storage
└── Test/                               # Test code folder
    ├── MovieTests.cs                   # Movie class test file
    ├── MemberTests.cs                  # Member class test file
    ├── MovieCollectionTests.cs         # MovieCollection class test file
    └── MemberCollectionTests.cs        # MemberCollection class test file
```

## Implementation Details

### Data Management

- All data is store in text file using simple comma-separated format:
  - **movies.txt**: Store movie information (title, genre, classification, duration, copies)
  - **members.txt**: Store member information (name, contact, login detail, borrowed movie)
- System do automatic data saving when program exit

### Key Algorithms

- **Hash Table**: Fast movie data management with linear probing for collision solving
- **Merge Sort**: Use for sorting popular movie by borrow count
- **Search Algorithm**: Many search implementation for quick data finding

## How to Use

### Installation

1. Copy this repository to your computer
2. Open solution file (DVDManagement.sln) in Visual Studio
3. Build project with .NET 7.0 SDK

### Running the Application

1. Start application from Visual Studio or run exe file
2. Choose Staff or Member login from main menu
3. For staff access, use this login:
   - Username: `staff`
   - Password: `today123`

### Staff Operations

- Add, remove, and update movie list
- Register and manage member account
- Check movie borrowing pattern

### Member Operations

- Browse available movies
- Borrow and return movies
- View personal borrowing history

## Testing

- Comprehensive test suite included in the Test folder
- Unit tests cover core functionality for both Movie and Member classes
- Collection management tests ensure data integrity
