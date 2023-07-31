using System.Data;
using Oracle.DataAccess.Client;


namespace FYPMSWebsite.App_Code
{
    /// <summary>
    /// Student name: LOI Hong Ching Henry
    /// Student number: 20852124
    /// 
    /// NOTE: This is an individual task. By submitting this file you certify that this
    /// code is the result of YOUR INDIVIDUAL EFFORT and that it has not been developed
    /// in collaoration with or copied from any other person. If this is not the case,
    /// then you must identify by name all the persons with whom you collaborated or
    /// from whom you copied code below.
    /// 
    /// Collaborators: 
    /// </summary>

    public class FYPMSDBAccess
    {
        //******************************** IMPORTANT NOTE ********************************
        // For the web pages to display a query result correctly, and possibly to not    *
        // generate errors, the attributes should be retrieved in the order specified,   *
        // if any, in a TODO and the attribute names in a query result table must be     *
        // EXACTLY the same as that in the database tables.                              *
        //                                                                               *
        //   REMINDER: DO NOT place single quotes around numeric data type parameters.   *
        //                                                                               *
        //          Report problems with the website code to 3311rep@cse.ust.hk.         *
        //********************************************************************************

        private readonly OracleDBAccess myOracleDBAccess = new OracleDBAccess();
        private DataTable queryResult = null;
        private decimal aggregateQueryResult = -1;
        private bool updateResult = false;
        private string sql;
        //********************************************************************************

        #region SQL statements for FYP Webpages - TODOs 01 to 04

        public DataTable GetFYPDigests() // TODO 01
        {
            //********************************************************************************
            // TODO 01: Construct an SQL statement to retrieve the fyp id, title, category,  *
            //          type, minimum students, maximum students and status for all FYPs.    *
            //   Order: title ascending                                                      *
            //********************************************************************************
            sql = $"select fypId, title, category, type, minStudents, maxStudents, status "
                  + $"from FYP "
                  + $"order by title";
            return queryResult = myOracleDBAccess.GetData("TODO 01", sql);
        }

        public DataTable GetFYPDetails(string fypId) // TODO 02
        {
            //********************************************************************************
            // TODO 02: Construct an SQL statement to retrieve the FYP record for the FYP    *
            //          identified by an fyp id.                                             *
            //********************************************************************************
            sql = $"select * "
                  + $"from FYP "
                  + $"where fypId={fypId}";
            return queryResult = myOracleDBAccess.GetData("TODO 02", sql);
        }

        public DataTable GetFYPSupervisors(string fypId) // TODO 03
        {
            //********************************************************************************
            // TODO 03: Construct an SQL statement to retrieve the username, first name and  *
            //          last name of the supervisors of the FYP identified by an fyp id.     *
            //   Order: last name ascending, first name ascending                            *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled NAME with a space separating the first and last names       *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled NAME with value "Carl Chan".             *
            //********************************************************************************
            sql = $"select username, firstName || ' ' || lastName as name "
                  + $"from Faculty natural join Supervises "
                  + $"where fypId={fypId} "
                  + $"order by lastName, firstname";
            return queryResult = myOracleDBAccess.GetData("TODO 03", sql);
        }

        public DataTable GetFYPCategories() // TODO 04
        {
            //********************************************************************************
            // TODO 04: Construct an SQL statement to retrieve all the distinct FYP          *
            //          categories in the FYP records.                                       *
            //   Order: category ascending                                                   *
            //********************************************************************************
            sql = $"select distinct category "
                  + $"from FYP "
                  + $"order by category";
            return queryResult = myOracleDBAccess.GetData("TODO 04", sql);
        }

        #endregion SQL statements for FYP Webpages - TODOs 01 to 04

        #region SQL Statements for Faculty Webpages - TODOS 05 to 27

        public DataTable GetSupervisorFYPDigest(string username) // TODO 05
        {
            //********************************************************************************
            // TODO 05: Construct an SQL statement to retrieve the fyp id, title, category,  *
            //          type, minimum students, maximum students and status of the FYPs      *
            //          supervised by a faculty identified by a username.                    *
            //   Order: title ascending                                                      *
            //********************************************************************************
            sql = $"Select fypId, title, category, type, minStudents, maxStudents, status "
                  + $"from FYP natural join Supervises "
                  + $"where username='{username}' "
                  + $"order by title";
            return queryResult = myOracleDBAccess.GetData("TODO 05", sql);
        }

        public DataTable GetInterestedInFYP(string fypId) // TODO 06
        {
            //********************************************************************************
            // TODO 06: Construct an SQL statement to retrieve the fyp id, group id and      *
            //          priority for all groups that have indicated an interest in an FYP    *
            //          identified by an fyp id.                                             *
            //********************************************************************************
            sql = $"Select fypId, groupId, priority " +
                $"from InterestedIn " +
                $"where fypId={fypId} ";
            return queryResult = myOracleDBAccess.GetData("TODO 06", sql);
        }

        public DataTable GetFaculty() // TODO 07
        {
            //********************************************************************************
            // TODO 07: Construct an SQL statement to retrieve the username, first name and  *
            //          last name of all faculty.                                            *
            //   Order: last name ascending, first name ascending                            *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled NAME with a space separating the first and last names       *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled NAME with value "Carl Chan".             *
            //********************************************************************************
            sql = $"Select username, (firstName || ' ' || lastName) as NAME " +
                $"from Faculty " +
                $"order by lastName asc, firstName asc";
            return queryResult = myOracleDBAccess.GetData("TODO 07", sql);
        }

        public DataTable GetCosupervisor(string fypId, string username) // TODO 08
        {
            //********************************************************************************
            // TODO 08: Construct an SQL statement to retrieve the username of the           *
            //          cosupervisor, if any, of an FYP, identified by an fyp id, given the  *
            //          username of the other supervisor.                                    *
            //********************************************************************************
            sql = $"Select username " +
                $"from Supervises natural join Faculty " +
                $"where fypId={fypId} and username <> '{username}'";
            return queryResult = myOracleDBAccess.GetData("TODO 08", sql);
        }

        public bool CreateFYP(string fypId, string title, string description, string category,
            string type, string otherRequirements, string minStudents, string maxStudents,
            string status, OracleTransaction trans) // TODO 09
        {
            //********************************************************************************
            // TODO 09: Construct an SQL statement to create an FYP record.                  *
            //********************************************************************************
            sql = $"Insert into FYP values({fypId}, '{title}', '{description}', '{category}', '{type}', '{otherRequirements}', {minStudents}, {maxStudents}, '{status}')";
            return updateResult = myOracleDBAccess.SetData("TODO 09", sql, trans);
        }

        public bool UpdateFYP(string fypId, string title, string description, string category,
            string type, string otherRequirements, string minStudents, string maxStudents,
            string status, OracleTransaction trans) // TODO 10
        {
            //********************************************************************************
            // TODO 10: Construct an SQL statement to update all FYP record values for an    *
            //          FYP identified by an fyp id.                                         *
            //********************************************************************************
            sql = $"Update FYP set title='{title}', description='{description}', category='{category}', type='{type}', otherRequirements='{otherRequirements}', minStudents={minStudents}, maxStudents={maxStudents}, status='{status}' where fypId={fypId}";
            return updateResult = myOracleDBAccess.SetData("TODO 10", sql, trans);
        }

        public bool AddSupervisor(string username, string fypId, OracleTransaction trans) // TODO 11
        {
            //********************************************************************************
            // TODO 11: Construct an SQL statement to add a supervisor, identified by a      *
            //          username, to an FYP identified by an fyp id.                         *
            //********************************************************************************
            sql = $"Insert into Supervises values('{username}', {fypId})";
            return updateResult = myOracleDBAccess.SetData("TODO 11", sql, trans);
        }

        public bool RemoveSupervisor(string username, string fypId, OracleTransaction trans) // TODO 12
        {
            //********************************************************************************
            // TODO 12: Construct an SQL statement to remove a supervisor, identified by     *
            //          a username, from an FYP identified by an fyp id.                     *
            //********************************************************************************
            sql = $"Delete from Supervises where fypId={fypId} and username='{username}'";
            return updateResult = myOracleDBAccess.SetData("TODO 12", sql, trans);
        }

        public DataTable GetSupervisorProjectGroups(string username) // TODO 13
        {
            //********************************************************************************
            // TODO 13: Construct an SQL statement to retrieve the group id, group code and  *
            //          assigned fyp id of the project groups supervised by a faculty        *
            //          identified by a username.                                            *
            //   Order: group code ascending                                                 *
            //********************************************************************************
            sql = $"Select groupId, groupCode, assignedFypId " +
                $"from ProjectGroup p join Supervises s on s.fypId = p.assignedFypId " +
                $"where username='{username}' " +
                $"order by groupCode asc";
            return queryResult = myOracleDBAccess.GetData("TODO 13", sql);
        }

        public DataTable GetFacultyFYPs(string username) // TODO 14
        {
            //********************************************************************************
            // TODO 14: Construct an SQL statement to retrieve the id and title of the       *
            //          projects supervised by a faculty identified by a username.           *
            //   Order: title ascending                                                      *
            //********************************************************************************
            sql = $"Select fypId, title " +
                $"from FYP natural join Supervises " +
                $"where username='{username}' " +
                $"order by title asc";
            return queryResult = myOracleDBAccess.GetData("TODO 14", sql);
        }

        public DataTable GetFYPStatus(string fypId) // TODO 15
        {
            //********************************************************************************
            // TODO 15: Construct an SQL statement to retrieve the status of an FYP          *
            //          identified by an fyp id.                                             *
            //********************************************************************************
            sql = $"Select status " +
                $"from FYP " +
                $"where fypId={fypId}";
            return queryResult = myOracleDBAccess.GetData("TODO 15", sql);
        }

        public DataTable GetGroupsAvailableToAssignToFYP(string fypId) // TODO 16
        {
            //********************************************************************************
            // TODO 16: Construct an SQL statement to retrieve the group id, priority and    *
            //          the first name and last name of all students in the group, for those *
            //          groups that have indicated an interest in the FYP, identified by an  *
            //          fyp id, and that are available for assignment.                       *
            //   Order: priority ascending, last name ascending, first name ascending        *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled MEMBERS with a space separating the first and last names    *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled MEMBERS with value "Carl Chan".          *
            //********************************************************************************
            sql = $"Select groupId, priority, (firstName || ' ' || lastName) as MEMBERS " +
                $"from InterestedIn natural join ProjectGroup natural join CSEStudent " +
                $"where fypId={fypId} and assignedFypId is NULL " +
                $"order by priority asc, lastName asc, firstName asc";
            return queryResult = myOracleDBAccess.GetData("TODO 16", sql);
        }

        public DataTable GetGroupsAssignedToFYP(string fypId) // TODO 17
        {
            //********************************************************************************
            // TODO 17: Construct an SQL statement to retrieve the group id, group code, priority, *
            //          the first and last names of all the students in the group and the    *
            //          first and last names of the reader for the project, if any, for those*
            //          groups that have been assigned to the FYP identified by an fyp id.   *
            //   Order: group code ascending, student last name ascending, student first     *
            //          name ascending                                                       *
            // ----------------------------------------------------------------------------- *
            //    NOTE: For students the first and last names should be returned as a single *
            //          attribute labelled MEMBERS with a space separating the first and     *
            //          last names (e.g., first name "Carl" and last name "Chan" should be   *
            //          returned as a single attribute labelled MEMBERS with value "Carl     *
            //          Chan". Similarly, for readers the first and last names should be     *
            //          returned as a single attribute labelled READER with a space          *
            //          separating the first and last names.                                 *
            //********************************************************************************
            sql = $" Select groupId, groupCode, priority, (s.firstName || ' ' || s.lastName) AS MEMBERS, (F.firstName || ' ' || F.lastName) AS READER " +
                $" from ProjectGroup pg " +
                $" join CSEStudent s using (groupId) " +
                $" natural join InterestedIn " +
                $" join Faculty F ON pg.reader = F.username " +
                $" where assignedFypId = {fypId} " +
                $" order by pg.groupCode ASC, s.lastName ASC, s.firstName ASC ";
            return queryResult = myOracleDBAccess.GetData("TODO 17", sql);
        }

        public DataTable GetFYPSupervisorFacultyCodes(string fypId) // TODO 18
        {
            //********************************************************************************
            // TODO 18: Construct an SQL statement to retrieve the faculty codes of all the  *
            //          supervisors of an FYP identified by an fyp id.                       *
            //   Order: faculty code ascending                                               *
            //********************************************************************************
            sql = $"Select facultyCode " +
                $"from Supervises natural join Faculty " +
                $"where fypId = {fypId}  " +
                $"order by facultyCode asc";
            return queryResult = myOracleDBAccess.GetData("TODO 18", sql);
        }

        public decimal GetGroupCodePrefixCount(string groupCodePrefix) // TODO 19
        {
            //********************************************************************************
            // TODO 19: Construct an SQL statement to retrieve the number of times a given   *
            //          group code prefix has been used. A group code prefix is the group    *
            //          code minus its trailing integer (e.g., for group code "FL1" the      *
            //          prefix is "FL"; for group code "FLJK3" the prefix is "FLJK").        *
            //********************************************************************************
            sql = $"Select count(*) from ProjectGroup where regexp_like(groupCode, '^{groupCodePrefix}[0-9]')";
            return aggregateQueryResult = myOracleDBAccess.GetAggregateValue("TODO 19", sql);
        }

        public bool AssignGroupToFYP(string groupId, string fypId, string groupCode) // TODO 20
        {
            //********************************************************************************
            // TODO 20: Construct an SQL statement to assign a project group, identified by  *
            //          a group id, to an FYP, identified by an fyp id, with a specified     *
            //          group code.                                                          *
            //********************************************************************************
             // MY TODO: *** SQL error in TODO 20: ORA-00001: unique constraint (COMP3311STU031.SYS_C001370963) violated.
            sql = $"Update ProjectGroup set assignedFypId={fypId}, groupCode='{groupCode}' where groupId={groupId}";
            return updateResult = myOracleDBAccess.SetData("TODO 20", sql);
        }

        public DataTable GetReaderProjectGroups(string username) // TODO 21
        {
            //********************************************************************************
            // TODO 21: Construct an SQL statement to retrieve the group id, group code and  *
            //          fyp id of the project groups to which a faculty, identified by a     *
            //          username, has been assigned as a reader.                             *
            //   Order: group code ascending                                                 *
            //********************************************************************************
            sql = $"Select groupId, groupCode, pg.assignedFypId " +
                $"from ProjectGroup pg join FYP fyp on pg.assignedFypId = fyp.fypId " +
                $"where reader='{username}' " +
                $"order by groupCode asc";
            return queryResult = myOracleDBAccess.GetData("TODO 21", sql);
        }

        public DataTable GetSupervisorRequirementGrades(string groupId, string fypId) // TODO 22
        {
            //********************************************************************************
            // TODO 22: Construct an SQL statement to retrieve, for all the students in a    *
            //          project group, identified by its group id, the username, first name, *
            //          last name and all their requirement grades given by any of the       *
            //          faculty supervising the FYP identified by an fyp id.                 *
            //   Order: last name ascending, first name ascending                            *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled NAME with a space separating the first and last names       *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled NAME with value "Carl Chan".             *
            //********************************************************************************
            // MY TODO: check
            sql = $"Select st.username, (firstName || ' ' || lastName) as NAME, proposalReport, progressReport, finalReport, presentation " +
                $"from CSEStudent st natural join ProjectGroup PG join Supervises S on PG.assignedFypId = S.fypId join RequirementGrades R on R.studentUsername = st.username " +
                $"where groupId={groupId} and assignedFypId={fypId} and PG.reader <> R.facultyUsername " +
                $"order by lastName asc, firstName asc";
            return queryResult = myOracleDBAccess.GetData("TODO 22", sql);
        }

        public DataTable GetReaderRequirementGrades(string groupId, string username) // TODO 23
        {
            //********************************************************************************
            // TODO 23: Construct an SQL statement to retrieve, for all the students in a    *
            //          project group, identified by its group id, the username, first and   *
            //          last names and all their requirement grades given by the reader of   *
            //          the project identified by a username.                                *
            //   Order: last name ascending, first name ascending                            *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled NAME with a space separating the first and last names       *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled NAME with value "Carl Chan".             *
            //********************************************************************************
            // MY TODO: check
            sql = $"Select username, (st.firstName || ' ' || st.lastName) as NAME, proposalReport, progressReport, finalReport, presentation " +
                $"from CSEStudent st natural join ProjectGroup P join RequirementGrades R on R.studentUsername = st.username " +
                $"where groupId={groupId} and reader='{username}' and P.reader = R.facultyUsername " +
                $"order by lastName asc, firstName asc";
            return queryResult = myOracleDBAccess.GetData("TODO 23", sql);
        }

        public DataTable GetProjectGroupMembers(string groupId) // TODO 24
        {
            //********************************************************************************
            // TODO 24: Construct an SQL statement to retrieve the username, last and first  *
            //          names and group id of all the students in a project group identified *
            //          by its group id.                                                     *
            //   Order: last name ascending, first name ascending                            *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled NAME with a space separating the first and last names       *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled NAME with value "Carl Chan".             *
            //********************************************************************************
            sql = $"Select username, (firstName || ' ' || lastName) as NAME, groupId " +
                $"from CSEStudent " +
                $"where groupId = {groupId} " +
                $"order by lastName asc, firstName asc";
            return queryResult = myOracleDBAccess.GetData("TODO 24", sql);
        }

        public bool CreateRequirementGrades(string facultyUsername, string studentUsername, string proposalReport,
            string progressReport, string finalReport, string presentation, OracleTransaction trans) // TODO 25
        {
            //********************************************************************************
            // TODO 25: Construct an SQL statement to insert a value for each requirement    *
            //          grade for a student, identified by a username, given by a faculty    *
            //          identified by a username.                                            *
            //********************************************************************************
            sql = $"Insert into RequirementGrades values('{facultyUsername}', '{studentUsername}', {proposalReport}, {progressReport}, {finalReport}, {presentation})";
            return updateResult = myOracleDBAccess.SetData("TODO 25", sql, trans);
        }

        public bool UpdateSupervisorRequirementGrades(string fypId, string studentUsername,
            string proposalReport, string progressReport, string finalReport, string presentation) // TODO 26
        {
            //********************************************************************************
            // TODO 26: Construct an SQL statement to update all the requirement grades      *
            //          given by a supervisor of an FYP, identified by its fyp id, to a      *
            //          student identified by a username.                                    *
            // ----------------------------------------------------------------------------- *
            //    NOTE: While a student's grades can be updated by any of the supervisors of *
            //          an FYP, the username of only one of the supervisors appears in a     *
            //          RequirementGrades record.                                            *
            //********************************************************************************
            sql = $"Update RequirementGrades set proposalReport={proposalReport},progressReport={progressReport}, finalReport= {finalReport}, presentation={presentation} where studentUsername='{studentUsername}' and facultyUsername in (Select username from Supervises where fypId={fypId})";
            return updateResult = myOracleDBAccess.SetData("TODO 26", sql);
        }

        public bool UpdateReaderRequirementGrades(string facultyUsername, string studentUsername,
            string proposalReport, string progressReport, string finalReport, string presentation) // TODO 27
        {
            //********************************************************************************
            // TODO 27: Construct an SQL statement to update all the requirement grades      *
            //          given by a reader, identified by a username, to a student identified *
            //          by a username.                                                       *
            //********************************************************************************
            sql = $"Update RequirementGrades set proposalReport='{proposalReport}',progressReport='{progressReport}', finalReport= '{finalReport}', presentation='{presentation}' where facultyUsername='{facultyUsername}' and studentUsername='{studentUsername}'";
            return updateResult = myOracleDBAccess.SetData("TODO 27", sql);
        }

        #endregion SQL Statements for Faculty Webpages - TODOS 05 to 27

        #region SQL Statements for Student Webpages TODOS 28 - 41

        public DataTable GetStudentGroupId(string username) // TODO 28
        {
            //********************************************************************************
            // TODO 28: Construct an SQL statement to retrieve the group id for the student  *
            //          identified by a username.                                            *
            //********************************************************************************
            sql = $"Select groupId " +
                $"from CSEStudent " +
                $"where username='{username}'";
            return queryResult = myOracleDBAccess.GetData("TODO 28", sql);
        }

        public DataTable GetAssignedFYPId(string groupId) // TODO 29
        {
            //********************************************************************************
            // TODO 29: Construct an SQL statement to retrieve the fyp id of the FYP to      *
            //          which a project group, identified by its group id, is assigned.      *
            //********************************************************************************
            sql = $"Select assignedFypId  " +
                $"from ProjectGroup " +
                $"where groupId={groupId}";
            return queryResult = myOracleDBAccess.GetData("TODO 29", sql);
        }

        public DataTable GetGroupAvailableFYPDigests(string groupId) // TODO 30
        {
            //********************************************************************************
            // TODO 30: Construct an SQL statement to retrieve the fyp id, title, category,  *
            //          type, minimum students and maximum students of the FYPs for which a  *
            //          group, identified by its group id, can indicate an interest.         *
            //   Order: title ascending                                                      *
            //********************************************************************************
            // MY TODO: Need to count number of students in group
            sql = $"Select fypId, title, category, type, minStudents, maxStudents " +
                $"from FYP " +
                $"where status='available' and fypId not in (Select fypId from InterestedIn where groupId={groupId}) " +
                $"order by title asc";
            return queryResult = myOracleDBAccess.GetData("TODO 30", sql);
        }

        public DataTable GetAssignedFYPTitle(string groupId) // TODO 31
        {
            //********************************************************************************
            // TODO 31: Construct an SQL statement to retrieve the title of the FYP assigned *
            //          to the group identified by its group id.                             *
            //********************************************************************************
            sql = $"Select title  " +
                $"from FYP fyp join ProjectGroup pg on pg.assignedFypId = fyp.fypId " +
                $"where groupId={groupId}";
            return queryResult = myOracleDBAccess.GetData("TODO 31", sql);
        }

        public bool IndicateInterestInFYP(string fypId, string groupId, string priority) // TODO 32
        {
            //********************************************************************************
            // TODO 32: Construct an SQL statement to indicate interest, with a specified    *
            //          priority, in an FYP, identified by an fyp id, by a project group     *
            //          identified by its group id.                                          *
            //********************************************************************************
            sql = $"Insert into InterestedIn values({fypId}, {groupId}, {priority})";
            return updateResult = myOracleDBAccess.SetData("TODO 32", sql);
        }

        public DataTable GetFYPsGroupHasIndicatedInterestIn(string groupId) // TODO 33
        {
            //********************************************************************************
            // TODO 33: Construct an SQL statement to retrieve the fyp id, title, category,  *
            //          type, priority and status of all the FYPs for which a project group, *
            //          identified by its group id, has indicated an interest.               *
            //   Order: priority ascending, title ascending                                  *
            //********************************************************************************
            sql = $"Select fypId, title, category, type, priority, status " +
                $"from ProjectGroup pg natural join InterestedIn natural join FYP fyp " +
                $"where groupId={groupId} " +
                $"order by priority asc, title asc";
            return queryResult = myOracleDBAccess.GetData("TODO 33", sql);
        }

        public DataTable GetStudentRecord(string username) // TODO 34
        {
            //********************************************************************************
            // TODO 34: Construct an SQL statement to retrieve the record of a student       *
            //          identified by a username.                                            *
            //********************************************************************************
            sql = $"Select *  " +
                $"from CSEStudent " +
                $"where username = '{username}'";
            return queryResult = myOracleDBAccess.GetData("TODO 34", sql);
        }

        public bool CreateProjectGroup(string groupId) // TODO 35
        {
            //********************************************************************************
            // TODO 35: Construct an SQL statement to create a project group with a          *
            //          specified group id.                                                  *
            //********************************************************************************
            sql = $"Insert into ProjectGroup values({groupId}, NULL, NULL, NULL)";
            return updateResult = myOracleDBAccess.SetData("TODO 35", sql);
        }

        public bool AddStudentToProjectGroup(string username, string groupId) // TODO 36
        {
            //********************************************************************************
            // TODO 36: Construct an SQL statement to assign a student, identified by a      *
            //          username, to a project group identified by a group id.               *
            //********************************************************************************
            sql = $"Update CSEStudent set groupId={groupId} where username='{username}'";
            return updateResult = myOracleDBAccess.SetData("TODO 36", sql);
        }

        public bool RemoveStudentFromProjectGroup(string username) // TODO 37
        {
            //********************************************************************************
            // TODO 37: Construct an SQL statement to remove a student, identified by a      *
            //          username, from a project group.                                      *
            //********************************************************************************
            sql = $"Update CSEStudent set groupId = NULL where username='{username}' ";
            return updateResult = myOracleDBAccess.SetData("TODO 37", sql);
        }

        public bool DeleteProjectGroup(string groupId) // TODO 38
        {
            //********************************************************************************
            // TODO 38: Construct an SQL statement to delete a project group identified by   *
            //          its group id.                                                        *
            //********************************************************************************
            sql = $"Delete from ProjectGroup where groupId = {groupId}";
            return updateResult = myOracleDBAccess.SetData("TODO 38", sql);
        }

        public DataTable GetAssignedFYPInformation(string username) // TODO 39
        {
            //********************************************************************************
            // TODO 39: Construct an SQL statement to retrieve the fyp id, title and reader  *
            //          username for the FYP to which a student, identified by a username,   *
            //          is assigned.                                                         *
            //********************************************************************************
            sql = $"Select fypId, title, reader " +
                $"from ProjectGroup pg join FYP fyp on pg.assignedFypId = fyp.fypId " +
                $"where groupId=(Select groupId from CSEStudent where username='{username}')";
            return queryResult = myOracleDBAccess.GetData("TODO 39", sql);
        }

        public DataTable GetProjectGroupReaderName(string username) // TODO 40
        {
            //********************************************************************************
            // TODO 40: Construct an SQL statement to retrieve the reader first and last     *
            //          names for a reader identified a username.                            *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled NAME with a space separating the first and last names       *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled NAME with value "Carl Chan".             *
            //********************************************************************************
            sql = $"Select (firstName || ' ' || lastName) as NAME " +
                $"from Faculty " +
                $"where username = '{username}'";
            return queryResult = myOracleDBAccess.GetData("TODO 40", sql);
        }

        public DataTable GetStudentGrades(string facultyUsername, string studentUsername) // TODO 41
        {
            //********************************************************************************
            // TODO 41: Construct an SQL statement to retrieve the proposal report, progress *
            //          report, final report and presentation grades given by a faculty,     *
            //          identified by a username, to a student identified by a username.     *
            //********************************************************************************
            sql = $"Select proposalReport, progressReport, finalReport, presentation " +
                $"from RequirementGrades " +
                $"where studentUsername='{studentUsername}' and facultyUsername='{facultyUsername}'";
            return queryResult = myOracleDBAccess.GetData("TODO 41", sql);
        }

        #endregion SQL Statements for Student Webpages TODOS 28 - 41

        #region SQL Statements for Coordinator Webpages TODOS 42 - 46

        public DataTable GetProjectGroupsWithoutReaders() // TODO 42
        {
            //********************************************************************************
            // TODO 42: Construct an SQL statement to retrieve the group id, group code,     *
            //          assigned fyp id as well as the FYP title, category and type for the  *
            //          project groups that do not have an assigned reader.                  *
            //   Order: group code ascending                                                 *
            //********************************************************************************
            sql = $"Select groupId, groupCode, pg.assignedFypId, title, category, type " +
                $"from ProjectGroup pg join FYP fyp on pg.assignedFypId = fyp.fypId " +
                $"where reader is NULL " +
                $"order by groupCode asc";
            return queryResult = myOracleDBAccess.GetData("TODO 42", sql);
        }

        public decimal GetNumberOfProjectGroupsAssignedToReader(string username) // TODO 43
        {
            //********************************************************************************
            // TODO 43: Construct an SQL statement to retrieve the number of project groups  *
            //          to which a faculty, identified by a username, is assigned as reader. *
            //********************************************************************************
            sql = $"Select count(*) " +
                $"from ProjectGroup " +
                $"where reader ='{username}'";
            return aggregateQueryResult = myOracleDBAccess.GetAggregateValue("TODO 43", sql);
        }

        public bool AssignReaderToProjectGroup(string username, string groupId) // TODO 44
        {
            //********************************************************************************
            // TODO 44: Construct an SQL statement to assign a reader, identified by a       *
            //          username, to a project group identified by its group id.             *
            //********************************************************************************
            sql = $"Update ProjectGroup set reader='{username}' where groupId={groupId}";
            return updateResult = myOracleDBAccess.SetData("TODO 44", sql);
        }

        public DataTable GetAssignedReaders() // TODO 45
        {
            //********************************************************************************
            // TODO 45: Construct an SQL statement to retrieve the reader first and last     *
            //          names, group code and FYP title for the project groups that have     *
            //          readers assigned to them.                                            *
            //   Order: last name ascending, group code ascending                            *
            // ----------------------------------------------------------------------------- *
            //    NOTE: The first and last names should be returned as a single attribute    *
            //          labelled NAME with a space separating the first and last names       *
            //          (e.g., first name "Carl" and last name "Chan" should be returned as  *
            //          a single attribute labelled NAME with value "Carl Chan".             *
            //********************************************************************************
            // MY TODO: check
            sql = $"Select (firstName || ' ' || lastName) as Name, groupCode, title " +
                $"from FYP fyp join ProjectGroup pg on fyp.fypId = pg.assignedFypId join Faculty f on pg.reader = f.username " +
                $"where reader is not NULL " +
                $"order by lastName asc, groupCode asc";
            return queryResult = myOracleDBAccess.GetData("TODO 45", sql);
        }

        public DataTable GetAssignedProjectGroups() // TODO 46
        {
            //********************************************************************************
            // TODO 46: Construct an SQL statement to retrieve the group id, group code,     *
            //          assigned fyp id and reader of the project groups assigned to an FYP. *
            //   Order: group code ascending                                                 *
            //********************************************************************************
            sql = $"Select distinct groupId, groupCode, assignedFypId, reader " +
                $"from ProjectGroup pg join Faculty f on f.username = pg.reader " +
                $"where assignedFypId is not NULL " +
                $"order by groupCode asc";
            return queryResult = myOracleDBAccess.GetData("TODO 46", sql);
        }

        #endregion SQL Statements for Coordinator Webpages TODOS 42 - 46
    }
}
