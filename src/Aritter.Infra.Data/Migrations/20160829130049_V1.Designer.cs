﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Aritter.Infra.Data;

namespace Aritter.Infra.Data.Migrations
{
    [DbContext(typeof(AritterContext))]
    [Migration("20160829130049_V1")]
    partial class V1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Authorization", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Allowed");

                    b.Property<bool>("Denied");

                    b.Property<int>("PermissionId");

                    b.Property<Guid>("UID");

                    b.Property<int>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("PermissionId")
                        .HasName("IX_Authorizations_PermissionId");

                    b.HasIndex("UserRoleId")
                        .HasName("IX_Authorizations_UserRoleId");

                    b.HasIndex("Id", "UserRoleId")
                        .IsUnique()
                        .HasName("IX_Authorizations_Id_UserRoleId");

                    b.ToTable("Authorizations");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("UID");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_Client_Name");

                    b.ToTable("Clients");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Operation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("UID");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_Operation_Name");

                    b.ToTable("Operations");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("OperationId");

                    b.Property<int>("ResourceId");

                    b.Property<Guid>("UID");

                    b.HasKey("Id");

                    b.HasIndex("OperationId");

                    b.HasIndex("ResourceId");

                    b.HasIndex("ResourceId", "OperationId")
                        .IsUnique()
                        .HasName("IX_Permissions_ResourceId_OperationId");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Resource", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("UID");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .HasName("IX_Resources_ClientId");

                    b.ToTable("Resources");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.UserAccount", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("InvalidLoginAttemptsCount");

                    b.Property<bool>("MustChangePassword");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 999);

                    b.Property<Guid>("UID");

                    b.Property<int?>("UserProfileId");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasName("IX_UserAccounts_Email");

                    b.HasIndex("UserProfileId")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique()
                        .HasName("IX_UserAccounts_Username");

                    b.ToTable("UserAccounts");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.UserAssignment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("UID");

                    b.Property<int>("UserAccountId");

                    b.Property<int>("UserRoleId");

                    b.HasKey("Id");

                    b.HasIndex("UserAccountId")
                        .HasName("IX_UserAssignments_UserAccountId");

                    b.HasIndex("UserRoleId")
                        .HasName("IX_UserAssignments_UserRoleId");

                    b.HasIndex("UserAccountId", "UserRoleId")
                        .IsUnique()
                        .HasName("IX_UserAssignments_UserAccountId_UserRoleId");

                    b.ToTable("UserAssignments");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.UserProfile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<Guid>("UID");

                    b.HasKey("Id");

                    b.ToTable("UserProfiles");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("ClientId");

                    b.Property<string>("Description")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<Guid>("UID");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("IX_UserRoles_Name");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Authorization", b =>
                {
                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.Permission", "Permission")
                        .WithMany("Authorizations")
                        .HasForeignKey("PermissionId");

                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.UserRole", "UserRole")
                        .WithMany("Authorizations")
                        .HasForeignKey("UserRoleId");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Operation", b =>
                {
                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.Client", "Client")
                        .WithMany("Operations")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_Operations_Clients");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Permission", b =>
                {
                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.Operation", "Operation")
                        .WithMany("Permissions")
                        .HasForeignKey("OperationId")
                        .HasConstraintName("FK_Permissions_Operations");

                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.Resource", "Resource")
                        .WithMany("Permissions")
                        .HasForeignKey("ResourceId");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.Resource", b =>
                {
                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.Client", "Client")
                        .WithMany("Resources")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_Resources_Clients");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.UserAccount", b =>
                {
                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.UserProfile", "UserProfile")
                        .WithOne("UserAccount")
                        .HasForeignKey("Aritter.Domain.SecurityModule.Aggregates.UserAccount", "UserProfileId");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.UserAssignment", b =>
                {
                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.UserAccount", "UserAccount")
                        .WithMany("Assignments")
                        .HasForeignKey("UserAccountId");

                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.UserRole", "UserRole")
                        .WithMany("UserAssignments")
                        .HasForeignKey("UserRoleId")
                        .HasConstraintName("FK_UserAssignments_UserRoles");
                });

            modelBuilder.Entity("Aritter.Domain.SecurityModule.Aggregates.UserRole", b =>
                {
                    b.HasOne("Aritter.Domain.SecurityModule.Aggregates.Client", "Client")
                        .WithMany("UserRoles")
                        .HasForeignKey("ClientId")
                        .HasConstraintName("FK_UserRoles_Clients");
                });
        }
    }
}