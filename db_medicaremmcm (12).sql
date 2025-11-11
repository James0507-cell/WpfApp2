-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Generation Time: Nov 11, 2025 at 03:44 PM
-- Server version: 10.4.32-MariaDB
-- PHP Version: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `db_medicaremmcm`
--

-- --------------------------------------------------------

--
-- Table structure for table `admin_activity_log`
--

CREATE TABLE `admin_activity_log` (
  `activity_id` int(11) NOT NULL,
  `admin_id` int(11) NOT NULL,
  `username` varchar(100) NOT NULL,
  `activity_type` varchar(50) NOT NULL,
  `activity_desc` text NOT NULL,
  `activity_date` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `admin_activity_log`
--

INSERT INTO `admin_activity_log` (`activity_id`, `admin_id`, `username`, `activity_type`, `activity_desc`, `activity_date`) VALUES
(6, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-20 12:15:48'),
(7, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-20 12:15:58'),
(8, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-20 12:16:03'),
(9, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1', '2025-10-20 12:16:08'),
(10, 6, 'lgarcia', 'Appointment Approved', 'Update Student + lgarcia', '2025-10-20 12:28:16'),
(11, 0, 'admin', 'Delete Student Info', 'Delete Student jcSantiago', '2025-10-20 12:37:17'),
(12, 0, 'admin', 'Delete Student Info', 'Delete Student jcSantiago', '2025-10-20 12:38:54'),
(13, 0, 'admin', 'Delete Student Info', 'Delete Student 2025003', '2025-10-20 12:45:05'),
(14, 1, 'admin', 'Update Student Info', 'Update Student 2025001', '2025-10-20 12:58:13'),
(15, 1, 'admin', 'Delete Student Info', 'Delete Student 2025004', '2025-10-20 12:59:40'),
(16, 1, 'admin', 'Medicine Inventory Updated', 'Added 3 units to medicine ID 25', '2025-10-20 13:30:40'),
(17, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-20 14:49:39'),
(18, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-20 14:49:50'),
(19, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-20 14:50:39'),
(20, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-20 14:58:51'),
(21, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 2', '2025-10-20 14:58:59'),
(22, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 3. Reason: no stock', '2025-10-20 14:59:08'),
(23, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-20 15:18:27'),
(24, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 37', '2025-10-20 17:08:37'),
(25, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 39', '2025-10-20 20:06:57'),
(26, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-21 00:50:48'),
(27, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-21 00:50:53'),
(28, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 4. Reason: sdad', '2025-10-21 00:51:01'),
(29, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-21 22:49:00'),
(30, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 46', '2025-10-21 22:49:13'),
(31, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 9. Reason: Bawal', '2025-10-21 22:49:26'),
(32, 1, 'admin', 'Medicine Inventory Updated', 'Added 5 units to medicine ID 25', '2025-10-21 22:50:54'),
(33, 1, 'admin', 'Medicine Inventory Updated', 'Added 3 units to medicine ID 25', '2025-10-21 22:51:00'),
(34, 0, 'admin', 'Appointment Approved', 'Add new Student + jmSantiago', '2025-10-22 01:16:42'),
(35, 1, 'admin', 'Update Student Info', 'Update Student 2022120096', '2025-10-22 01:24:48'),
(36, 0, 'admin', 'Appointment Approved', 'Add new Student + dadad', '2025-10-22 01:29:50'),
(37, 0, 'admin', 'Appointment Approved', 'Add new Student + fsfs', '2025-10-22 01:38:12'),
(38, 1, 'admin', 'Medicine Inventory Updated', 'Added 5 units to medicine ID 1', '2025-10-22 18:17:13'),
(39, 1, 'admin', 'Medicine Inventory Updated', 'Added 10 units to medicine ID 1', '2025-10-22 18:18:53'),
(40, 1, 'admin', 'Medicine Inventory Updated', 'Added 5 units to medicine ID 3', '2025-10-22 18:32:56'),
(41, 1, 'admin', 'Medicine Inventory Updated', 'Added 1 units to medicine ID 1', '2025-10-22 18:35:50'),
(42, 0, 'admin', 'Appointment Approved', 'Add new Student + weqeq', '2025-10-22 22:54:37'),
(43, 1, 'admin', 'Medicine Inventory Updated', 'Added 10 units to medicine ID 3', '2025-10-23 03:29:18'),
(44, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-23 13:59:16'),
(45, 1, 'admin', 'Medicine Inventory Updated', 'Added 1 units to medicine ID 7', '2025-10-23 14:00:59'),
(46, 1, 'admin', 'Medicine Inventory Updated', 'Added 30 units to medicine ID 7', '2025-10-23 14:01:22'),
(47, 1, 'admin', 'Medicine Inventory Updated', 'Added 1 units to medicine ID 7', '2025-10-23 14:01:31'),
(48, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 3', '2025-10-23 14:13:40'),
(49, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 4', '2025-10-23 14:13:57'),
(50, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-23 14:28:43'),
(51, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 43', '2025-10-23 14:28:50'),
(52, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 46', '2025-10-23 14:34:37'),
(53, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 49', '2025-10-23 15:06:15'),
(54, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 50', '2025-10-23 15:07:09'),
(55, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 50', '2025-10-23 15:08:23'),
(56, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 50', '2025-10-23 15:09:42'),
(57, 1, 'admin', 'Update Student Info', 'Update Student 2025007', '2025-10-23 22:11:52'),
(58, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 10', '2025-10-23 22:13:11'),
(59, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-23 22:13:56'),
(60, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-23 22:13:59'),
(61, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-23 22:14:05'),
(62, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-23 22:14:07'),
(63, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-23 22:14:12'),
(64, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-23 22:14:19'),
(65, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-23 22:14:20'),
(66, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-24 20:45:54'),
(67, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-24 20:48:44'),
(68, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-24 20:49:15'),
(69, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-24 20:51:44'),
(70, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-24 20:51:55'),
(71, 0, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1. Reason: dsdsa', '2025-10-24 21:09:06'),
(72, 0, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-24 21:09:50'),
(73, 0, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1. Reason: fsdfs', '2025-10-24 21:13:12'),
(74, 0, 'admin', 'Update Medcine Inventory', 'Added 5 units to medicine ID 1', '2025-10-24 21:24:15'),
(75, 0, 'admin', 'Update Medcine Inventory', 'Added 10 units to medicine ID 1', '2025-10-24 21:27:16'),
(76, 0, 'admin', 'Update Medcine Inventory', 'Added 23 units to medicine ID 1', '2025-10-24 21:28:10'),
(77, 0, 'admin', 'Update Medcine Inventory', 'Added 4 units to medicine ID 2', '2025-10-24 21:31:24'),
(78, 0, 'admin', 'Update Medcine Inventory', 'Added 4 units to medicine ID 1 to Inventory ID 23', '2025-10-24 21:44:20'),
(79, 0, 'admin', 'Update Medcine Inventory', 'Added 5 units to medicine ID 1', '2025-10-24 21:50:43'),
(80, 0, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 7', '2025-10-24 21:58:51'),
(81, 0, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-24 21:59:20'),
(82, 0, 'admin', 'Update Medcine Inventory', 'Added 5 units to medicine ID 2', '2025-10-24 21:59:34'),
(83, 0, 'admin', 'Update Medcine Inventory', 'Added 100 units to medicine ID 1', '2025-10-24 21:59:47'),
(84, 0, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 2. Reason: dsda', '2025-10-24 22:05:47'),
(85, 0, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 2', '2025-10-24 22:05:51'),
(86, 0, 'admin', 'Delete Student Info', 'Delete Student 2025002', '2025-10-24 22:46:44'),
(87, 0, 'admin', 'Delete Student Info', 'Delete Student 2025005', '2025-10-24 22:47:27'),
(88, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-25 04:17:06'),
(89, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-25 04:17:08'),
(90, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-25 04:17:15'),
(91, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-25 04:17:20'),
(92, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1. Reason: re', '2025-10-25 04:17:27'),
(93, 1, 'admin', 'Update Medcine Inventory', 'Added 5 units to medicine ID 1', '2025-10-25 04:17:42'),
(94, 1, 'admin', 'Update Student Info', 'Update Student 2025001', '2025-10-25 04:19:03'),
(96, 0, 'jdelacruz', 'Delete Student Info', 'Delete Student 2025009', '2025-10-25 04:22:36'),
(97, 0, 'admin', 'Add New Student', 'Add new Student + sdadas', '2025-10-25 04:23:15'),
(98, 1, 'admin', 'Delete Student Info', 'Delete Student 31231', '2025-10-25 04:30:28'),
(99, 1, 'admin', 'Add New Student', 'Add new Student + asdad', '2025-10-25 04:35:46'),
(100, 1, 'admin', 'Update Medcine Inventory', 'Added 5 units to medicine ID 2', '2025-10-25 04:36:29'),
(101, 1, 'admin', 'Update Medcine Inventory', 'Added 5 units to medicine ID 2', '2025-10-25 04:39:25'),
(102, 1, 'admin', 'Update Medicine Inventory', 'Added 2 units to medicine ID 2', '2025-10-25 04:41:21'),
(103, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 3', '2025-10-25 18:25:59'),
(104, 1, 'admin', 'Update Medicine Inventory', 'Added 5 units to medicine ID 1', '2025-10-25 18:26:19'),
(105, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 4', '2025-10-25 18:26:24'),
(106, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 4', '2025-10-25 18:26:25'),
(107, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 4', '2025-10-25 18:26:26'),
(108, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-27 01:46:36'),
(109, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 55', '2025-10-27 04:36:37'),
(110, 1, 'admin', 'Add New Student', 'Added new student: dsda', '2025-10-27 14:08:26'),
(111, 1, 'admin', 'Update Student Info', 'Update Student 20250015', '2025-10-27 14:22:20'),
(112, 1, 'admin', 'Update Student Info', 'Update Student 20250015', '2025-10-27 14:22:55'),
(113, 1, 'admin', 'Update Student Info', 'Update Student 20250013', '2025-10-27 14:24:40'),
(114, 1, 'admin', 'Update Student Info', 'Update Student 2025001', '2025-10-27 14:53:56'),
(115, 0, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 5', '2025-10-28 00:23:09'),
(116, 0, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-28 03:02:58'),
(117, 0, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-28 03:03:06'),
(118, 0, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1. Reason: dsfdf', '2025-10-28 03:03:13'),
(119, 0, 'admin', 'Update Medicine Inventory', 'Added 5 units to medicine ID 1', '2025-10-28 03:03:28'),
(120, 0, 'admin', 'Appointment Approved', 'Approved appointment ID 72', '2025-10-28 04:44:29'),
(121, 0, 'admin', 'Appointment Rejected', 'Rejected appointment ID 71', '2025-10-28 04:44:33'),
(122, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-28 13:14:25'),
(123, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-28 13:14:43'),
(124, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-10-28 13:15:33'),
(125, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-28 13:15:40'),
(126, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-28 13:15:52'),
(127, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-28 13:18:11'),
(128, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-28 13:18:21'),
(129, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-28 13:55:37'),
(130, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1. Reason: fsf', '2025-10-28 13:55:45'),
(131, 1, 'admin', 'Update Medicine Inventory', 'Added 5 units to medicine ID 1', '2025-10-28 13:55:57'),
(132, 1, 'admin', 'Update Student Info', 'Update Student 2025001', '2025-10-28 14:09:19'),
(133, 1, 'admin', 'Medicine Added', 'Added new medicine: Lagundi (250) with initial inventory of 20', '2025-10-28 15:00:43'),
(134, 1, 'admin', 'Medicine Added', 'Added new medicine: Rubitosin (500) with initial inventory of 20', '2025-10-28 15:04:52'),
(135, 1, 'admin', 'Medicine Added', 'Added new medicine: sdff (230) with initial inventory of 1', '2025-10-28 15:05:29'),
(136, 1, 'admin', 'Update Student Info', 'Update Student 2025006', '2025-10-28 15:08:07'),
(137, 1, 'admin', 'Medicine Added', 'Added new medicine: dasd (323) with initial inventory of 2', '2025-10-28 15:14:05'),
(138, 1, 'admin', 'Update Medicine Inventory', 'Added 20 units to medicine ID 14', '2025-10-31 01:35:41'),
(139, 1, 'admin', 'Update Medicine Inventory', 'Added 30 units to medicine ID 13', '2025-10-31 01:35:58'),
(140, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 6', '2025-10-31 15:16:44'),
(141, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-31 18:02:17'),
(142, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-10-31 18:02:20'),
(143, 1, 'admin', 'Delete Student Info', 'Delete Student 2025010', '2025-10-31 18:04:07'),
(144, 1, 'admin', 'Update Student Info', 'Update Student 2022323', '2025-10-31 18:09:08'),
(145, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-10-31 18:15:34'),
(146, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 77', '2025-10-31 18:15:45'),
(147, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 76', '2025-10-31 18:15:57'),
(148, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 75', '2025-10-31 18:16:05'),
(149, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 17', '2025-10-31 18:16:18'),
(150, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 16', '2025-10-31 18:16:24'),
(151, 1, 'admin', 'Add New Student', 'Added new student: dada', '2025-10-31 18:34:22'),
(152, 1, 'admin', 'Update Student Info', 'Update Student 2025008', '2025-10-31 18:47:42'),
(153, 1, 'admin', 'Add New Medicine', 'Added new medicine: test (150) with initial inventory of 19', '2025-10-31 19:03:26'),
(154, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 78', '2025-11-01 12:25:28'),
(155, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 19', '2025-11-01 12:25:38'),
(156, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 18', '2025-11-01 12:25:42'),
(157, 1, 'admin', 'Update Medicine Inventory', 'Added 5 units to medicine ID 1', '2025-11-01 12:25:58'),
(158, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 79', '2025-11-01 13:39:20'),
(159, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 20', '2025-11-01 13:53:42'),
(160, 1, 'admin', 'Medicine Request Rejected', 'Rejected medicine request ID 20', '2025-11-01 13:53:49'),
(161, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 20', '2025-11-01 13:54:00'),
(162, 1, 'admin', 'Update Medicine Info', 'Updated medicine (ID: 2) details to: Amoxicillin (450 mg).', '2025-11-01 16:10:41'),
(163, 1, 'admin', 'Update Medicine Info', 'Updated medicine (ID: 2) details to: Amoxicillin (500 mg).', '2025-11-01 16:16:48'),
(164, 1, 'admin', 'Update Medicine Info', 'Updated medicine (ID: 2) details to: Amoxicillin (450 mg).', '2025-11-01 16:17:35'),
(165, 1, 'admin', 'Update Medicine Info', 'Updated medicine (ID: 2) details to: Amoxicillin (500 mg).', '2025-11-01 16:17:42'),
(166, 1, 'admin', 'Appointment Approved', 'Approved appointment ID 82', '2025-11-02 20:36:18'),
(167, 1, 'admin', 'Appointment Rejected', 'Rejected appointment ID 82', '2025-11-02 20:36:30'),
(168, 1, 'Admin: admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-11-03 22:09:07'),
(169, 1, 'Admin: admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-11-03 22:09:18'),
(170, 1, 'Admin: admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1', '2025-11-03 22:09:27'),
(171, 1, 'Admin: admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-11-03 22:09:32'),
(172, 1, 'Admin: admin', 'Update Medicine Inventory', 'Added 5 units to medicine ID 1', '2025-11-03 22:09:42'),
(173, 1, 'admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-11-03 22:12:27'),
(174, 1, 'ADMIN: admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1', '2025-11-03 22:21:45'),
(175, 1, 'ADMIN: admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-11-03 22:24:33'),
(176, 1, 'ADMIN: admin', 'Medicine Request Rejected', 'Rejected medicine request ID 2', '2025-11-03 22:24:41'),
(177, 1, 'ADMIN: admin', 'Update Student Info', 'Update Student 2025005', '2025-11-03 22:24:56'),
(178, 1, 'ADMIN: admin', 'Delete Student Info', 'Delete Student 2113', '2025-11-03 22:25:19'),
(179, 1, 'ADMIN: admin', 'Update Medicine Inventory', 'Added 10 units to medicine ID 1', '2025-11-03 22:25:44'),
(180, 1, 'ADMIN: admin', 'Update Medicine Info', 'Updated medicine (ID: 1) details to: Biogesic (250 mg).', '2025-11-03 22:25:54'),
(181, 1, 'ADMIN: admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-11-03 23:45:01'),
(182, 1, 'ADMIN: admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-11-03 23:45:15'),
(183, 1, 'ADMIN: admin', 'Medicine Request Approved', 'Approved medicine request ID 1', '2025-11-03 23:45:28'),
(184, 1, 'ADMIN: admin', 'Medicine Request Rejected', 'Rejected medicine request ID 1', '2025-11-03 23:45:36'),
(185, 1, 'ADMIN: admin', 'Update Medicine Inventory', 'Added 5 units to medicine ID 1', '2025-11-03 23:47:16'),
(186, 1, 'ADMIN: admin', 'Appointment Approved', 'Approved appointment ID 86', '2025-11-05 01:02:10'),
(187, 1, 'ADMIN: admin', 'Appointment Approved', 'Approved appointment ID 87', '2025-11-06 09:06:01'),
(188, 1, 'ADMIN: admin', 'Appointment Rejected', 'Rejected appointment ID 88', '2025-11-06 09:07:02'),
(189, 1, 'ADMIN: admin', 'Update Student Info', 'Update Student 2025005', '2025-11-07 22:38:54'),
(190, 1, 'ADMIN: admin', 'Update Student Info', 'Update Student 2025005', '2025-11-07 22:39:29'),
(191, 1, 'ADMIN: admin', 'Appointment Approved', 'Approved appointment ID 25', '2025-11-07 23:18:28'),
(192, 1, 'ADMIN: admin', 'Appointment Rejected', 'Rejected appointment ID 25', '2025-11-07 23:18:35'),
(193, 1, 'ADMIN: admin', 'Medicine Request Approved', 'Approved medicine request ID 2', '2025-11-08 03:41:08'),
(194, 1, 'ADMIN: admin', 'Update Medicine Info', 'Updated medicine (ID: 2) details to: Amoxicillin (550 mg).', '2025-11-08 18:31:22'),
(195, 1, 'ADMIN: admin', 'Delete Student Info', 'Delete Student 12313', '2025-11-11 12:39:29'),
(196, 1, 'ADMIN: admin', 'Add New Student', 'Added new student: A', '2025-11-11 12:40:05');

-- --------------------------------------------------------

--
-- Table structure for table `appointments`
--

CREATE TABLE `appointments` (
  `appointment_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `student_id` varchar(20) DEFAULT NULL,
  `appointment_date` date NOT NULL,
  `appointment_time` time NOT NULL,
  `email` varchar(100) DEFAULT NULL,
  `phone_number` varchar(20) DEFAULT NULL,
  `purpose_of_visit` text DEFAULT NULL,
  `known_allergies` text DEFAULT NULL,
  `current_medication` text DEFAULT NULL,
  `previous_visit` enum('Yes','No') DEFAULT 'No',
  `emergency_contact_name` varchar(100) DEFAULT NULL,
  `emergency_contact_phone` varchar(20) DEFAULT NULL,
  `status` enum('Pending','Approved','Rejected') DEFAULT 'Pending',
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `current_symptoms` text DEFAULT NULL,
  `reason` text DEFAULT NULL,
  `handled_time` datetime DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `appointments`
--

INSERT INTO `appointments` (`appointment_id`, `user_id`, `username`, `student_id`, `appointment_date`, `appointment_time`, `email`, `phone_number`, `purpose_of_visit`, `known_allergies`, `current_medication`, `previous_visit`, `emergency_contact_name`, `emergency_contact_phone`, `status`, `created_at`, `current_symptoms`, `reason`, `handled_time`) VALUES
(25, 8, 'jdelacruz', '2025001', '2025-10-15', '17:00:00', 'juan.delacruz@example.com', '09171234567', 'Injury', 'dadad', 'dasd', 'Yes', 'dadad', '313123', 'Rejected', '2025-10-15 06:24:17', NULL, 'No doctor\r\n', '2025-11-07 23:18:35'),
(43, 8, 'jdelacruz', '2025001', '2025-10-21', '16:00:00', 'juan.delacruz@example.com', '09171234567', 'Injury', 'dsa', 'dsad', 'No', 'dasd', '123', 'Approved', '2025-10-20 16:20:17', 'dsad', NULL, '2025-10-23 14:28:50'),
(46, 8, 'jdelacruz', '2025001', '2025-10-22', '17:00:00', 'juan.delacruz@example.com', '09171234567', 'Check-up', 'dsada', 'dasd', 'Yes', 'dasd', '12323', 'Approved', '2025-10-21 09:40:24', 'dsd', 'Bawal', '2025-10-23 14:34:37'),
(47, 8, 'jdelacruz', '2025001', '2025-10-22', '16:00:00', 'juan.delacruz@example.com', '09171234567', 'Check-up', 'dadsada', 'dsda', 'No', 'dsda', '31231', 'Pending', '2025-10-21 18:30:06', 'dada', NULL, NULL),
(48, 8, 'jdelacruz', '2025001', '2025-10-23', '14:00:00', 'juan.delacruz@example.com', '09171234567', 'Check-up', 'SADA', 'DASD', 'No', 'DDS', 'DSAD', 'Pending', '2025-10-22 12:38:39', 'asS', NULL, NULL),
(49, 8, 'jdelacruz', '2025001', '2025-10-23', '16:00:00', 'juan.delacruz@example.com', '09171234567', 'Check-up', 'dada', 'dsda', 'No', 'dasda', '231', 'Approved', '2025-10-23 07:05:48', 'dsda', NULL, '2025-10-23 15:06:15'),
(50, 8, 'jdelacruz', '2025001', '2025-10-23', '16:30:00', 'juan.delacruz@example.com', '09171234567', 'Injury', 'sad', 'dasda', 'No', 'dadsa', '2313', 'Rejected', '2025-10-23 07:06:49', 'das', 'Bawal', '2025-10-23 15:09:42'),
(51, 8, 'jdelacruz', '2025001', '2025-10-23', '17:00:00', 'juan.delacruz@example.com', '09171234567', 'Injury', 'dfs', 'dfsf', 'Yes', 'fd', '2131', 'Pending', '2025-10-23 08:29:48', 'dsd', NULL, NULL),
(56, 8, 'jdelacruz', '2025001', '2025-10-27', '09:00:00', 'juan.delacruz@example.com', '09171234567', 'Check-up', 'fsdf', 'fsfs', 'No', 'sdad', '3123', 'Pending', '2025-10-26 21:20:50', 'fdfs', NULL, NULL),
(58, 8, 'jdelacruz', '2025001', '2025-10-27', '10:30:00', 'juan.delacruz@example.com', '09171234567', 'Check-up', 'fsdfds', 'fsf', 'Yes', 'sdad', '3123', 'Pending', '2025-10-26 21:26:13', 'fss', NULL, NULL),
(60, 8, 'jdelacruz', '2025001', '2025-10-27', '09:30:00', 'juan.delacruz@example.com', '09171234567', 'Injury', 'dad', 'dsda', 'No', 'sdad', '3123', 'Pending', '2025-10-26 21:28:14', 'dsada', NULL, NULL),
(62, 8, 'jdelacruz', '2025001', '2025-10-27', '10:00:00', 'juan.delacruz@example.com', '09171234567', 'Injury', 'dsda', 'dasda', 'No', 'sdad', '3123', 'Pending', '2025-10-27 04:07:13', 'dsda', NULL, NULL),
(64, 8, 'jdelacruz', '2025001', '2025-10-27', '13:00:00', 'juan.delacruz@example.com', '09171234567', '', '', '', '', 'sdad', '3123', 'Pending', '2025-10-27 04:08:57', '', NULL, NULL),
(65, 8, 'jdelacruz', '2025001', '2025-10-27', '14:00:00', 'juan.delacruz@example.com', '09171234567', '', '', '', '', 'sdad', '3123', 'Pending', '2025-10-27 04:09:12', '', NULL, NULL),
(66, 8, 'jdelacruz', '2025001', '2025-10-27', '13:30:00', 'juan.delacruz@example.com', '09171234567', 'Check-up', 'fdfsf', 'fdsf', 'Yes', 'sdad', '3123', 'Pending', '2025-10-27 04:10:22', 'fd', NULL, NULL),
(67, 8, 'jdelacruz', '2025001', '2025-10-27', '15:00:00', 'juan.delacruz@example.com', '09171234565', 'Check-up', 'daas', 'das', 'Yes', 'sdad', '3123', 'Pending', '2025-10-27 10:09:16', 'dasda', NULL, NULL),
(71, 8, 'jdelacruz', '2025001', '2025-10-28', '09:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'dsad', 'dada', 'Yes', 'sdad', '3123', 'Rejected', '2025-10-27 15:07:03', 'saafsdasda', 'dsda', '2025-10-28 04:44:33'),
(72, 8, 'jdelacruz', '2025001', '2025-10-28', '10:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'fdsf', 'sdfs', 'Yes', 'sdad', '3123', 'Approved', '2025-10-27 20:33:22', 'dsfsdf', NULL, '2025-10-28 04:44:29'),
(74, 8, 'jdelacruz', '2025001', '2025-10-28', '16:00:00', 'juan.delacruz@example.com', '09171234565', 'Check-up', 'sdfs', 'dfdsf', 'Yes', 'sdad', '3123', 'Pending', '2025-10-27 20:49:49', 'ffd', NULL, NULL),
(79, 8, 'jdelacruz', '2025008', '2025-11-03', '09:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'rwr', 'rew', 'No', 'sdad', '3123', 'Approved', '2025-11-01 05:39:03', 'rerw', NULL, '2025-11-01 13:39:20'),
(80, 8, 'jdelacruz', '2025008', '2025-11-03', '10:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'fsdfs', 'fsdf', 'Yes', 'sdad', '3123', 'Pending', '2025-11-01 06:07:18', 'fsdf', NULL, NULL),
(81, 8, 'jdelacruz', '2025008', '2025-11-03', '13:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'N/A', 'N/A', 'No', 'Marilen Santiago', '09221906789', 'Pending', '2025-11-02 04:41:20', 'My leg hurst badly', NULL, NULL),
(82, 8, 'jdelacruz', '2025008', '2025-11-03', '14:00:00', 'juan.delacruz@example.com', '09171234565', 'Check-up', 'dsaddasd', 'dsada', 'Yes', 'sdad', '3123', 'Rejected', '2025-11-02 12:34:49', 'dsda', 'sdasdadsd', '2025-11-02 20:36:30'),
(85, 8, 'jdelacruz', '2025005', '2025-11-04', '10:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'N/A', 'N/A', 'No', 'sdad', '3123', 'Pending', '2025-11-03 15:41:12', 'Leg Hurts', NULL, NULL),
(86, 8, 'jdelacruz', '2025005', '2025-11-05', '09:00:00', 'juan.delacruz@example.com', '09171234565', 'Check-up', 'dasd', 'dsd', 'Yes', 'sdad', '3123', 'Approved', '2025-11-04 16:58:39', 'dasd', NULL, '2025-11-05 01:02:10'),
(88, 8, 'jdelacruz', '2025005', '2025-11-17', '17:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'N/A', 'N/A', 'Yes', 'Marilen Santiago', '09702514629', 'Rejected', '2025-11-06 01:05:31', 'Fever', 'No available doctor for that time and date', '2025-11-06 09:07:02'),
(89, 8, 'jdelacruz', '2025005', '2025-11-06', '16:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'N/A', 'N/A', 'Yes', 'Marilen Santiago', '09702514629', 'Pending', '2025-11-06 01:42:53', 'Fever', NULL, NULL),
(90, 8, 'jdelacruz', '2025005', '2025-11-06', '13:00:00', 'juan.delacruz@example.com', '09171234565', 'Injury', 'N/A', 'N/A', 'Yes', 'Marilen Santiago', '09702514629', 'Pending', '2025-11-06 01:48:14', 'Leg Pain', NULL, NULL),
(92, 8, 'jdelacruz', '2025005', '2025-11-11', '17:00:00', 'juan.delacruz@example.com', '09171234565', 'Check-up', 'N/A', 'N/A', 'Yes', 'sdad', '3123', 'Pending', '2025-11-11 04:47:38', 'Headache', NULL, NULL),
(94, 8, 'jdelacruz', '2025005', '2025-11-12', '10:00:00', 'juan.delacruz@example.com', '09171234565', 'Check-up', 'sdd', 'adsa', 'Yes', 'sdad', '3123', 'Pending', '2025-11-11 08:36:44', 'dsad', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `blood_types`
--

CREATE TABLE `blood_types` (
  `blood_type_id` int(11) NOT NULL,
  `blood_type` varchar(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `blood_types`
--

INSERT INTO `blood_types` (`blood_type_id`, `blood_type`) VALUES
(1, 'A+'),
(2, 'A-'),
(3, 'B+'),
(4, 'B-'),
(5, 'AB+'),
(6, 'AB-'),
(7, 'O+'),
(8, 'O-');

-- --------------------------------------------------------

--
-- Table structure for table `checkups`
--

CREATE TABLE `checkups` (
  `checkup_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `height_cm` decimal(5,2) DEFAULT NULL,
  `weight_kg` decimal(5,2) DEFAULT NULL,
  `bmi` decimal(5,2) DEFAULT NULL,
  `recorded_at` timestamp NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `checkups`
--

INSERT INTO `checkups` (`checkup_id`, `user_id`, `height_cm`, `weight_kg`, `bmi`, `recorded_at`) VALUES
(1, 1, 166.00, 59.00, 21.41, '2025-10-10 15:26:48'),
(2, 8, 344.00, 50.00, 4.23, '2025-10-10 15:59:01'),
(3, 8, 344.00, 50.00, 4.23, '2025-10-10 15:59:03'),
(4, 8, 32.00, 332.00, 999.99, '2025-10-10 16:07:46'),
(5, 8, 323.00, 232.00, 22.24, '2025-10-10 16:08:24'),
(6, 8, 323.00, 232.00, 22.24, '2025-10-10 16:08:25'),
(7, 8, 312.00, 313.00, 32.15, '2025-10-10 16:11:29'),
(8, 8, 312.00, 313.00, 32.15, '2025-10-14 13:25:04'),
(9, 8, 312.00, 413.00, 42.43, '2025-10-14 13:25:46'),
(10, 8, 312.00, 313.00, 32.15, '2025-10-14 13:25:50'),
(11, 8, 168.00, 55.00, 19.49, '2025-10-14 13:34:01'),
(12, 8, 168.00, 100.00, 35.43, '2025-10-14 13:42:19'),
(13, 8, 170.00, 68.00, 23.53, '2025-10-14 14:29:13'),
(14, 8, 170.00, 65.00, 22.49, '2025-10-14 14:29:13'),
(15, 8, 170.00, 66.00, 34.00, '2025-09-14 14:29:13'),
(16, 8, 170.00, 64.00, 18.00, '2025-08-14 14:29:13'),
(17, 8, 170.00, 67.00, 26.00, '2025-07-14 14:29:13'),
(18, 8, 170.00, 68.00, 23.53, '2025-06-14 14:29:13'),
(19, 8, 170.00, 63.00, 21.80, '2025-05-14 14:29:13'),
(20, 8, 170.00, 58.00, 20.07, '2025-10-15 03:05:52'),
(21, 8, 170.00, 150.00, 51.90, '2025-10-15 03:09:06'),
(22, 8, 170.00, 55.00, 19.03, '2025-10-15 10:06:47'),
(23, 8, 120.00, 55.00, 38.19, '2025-10-15 10:07:01'),
(24, 8, 180.00, 55.00, 16.98, '2025-10-15 10:07:18'),
(25, 8, 150.00, 55.00, 24.44, '2025-10-15 10:07:24'),
(26, 8, 120.00, 55.00, 38.19, '2025-10-15 10:07:31'),
(27, 8, 120.00, 55.00, 38.19, '2025-10-15 10:07:31'),
(28, 8, 170.00, 57.00, 19.72, '2025-10-15 10:07:53'),
(29, 8, 170.00, 59.50, 20.59, '2025-10-16 12:18:10'),
(30, 8, 170.00, 58.00, 20.07, '2025-10-20 10:13:11'),
(31, 8, 212.00, 80.00, 17.80, '2025-10-21 09:06:45'),
(32, 8, 233.00, 90.00, 16.58, '2025-10-21 09:07:14'),
(33, 8, 150.00, 52.00, 23.11, '2025-10-21 14:47:30'),
(34, 8, 170.00, 65.00, 22.49, '2025-10-23 07:10:14'),
(35, 8, 168.00, 55.00, 19.49, '2025-10-27 18:16:16'),
(36, 8, 170.00, 58.00, 20.07, '2025-10-27 18:16:37'),
(37, 8, 170.00, 45.00, 15.57, '2025-10-27 18:23:30'),
(38, 8, 178.00, 70.00, 22.09, '2025-10-27 20:43:41'),
(39, 8, 170.00, 57.00, 19.72, '2025-10-27 20:50:39'),
(40, 8, 150.00, 40.00, 17.78, '2025-10-31 09:43:01'),
(41, 8, 170.00, 58.00, 20.07, '2025-11-01 04:23:58'),
(42, 8, 172.00, 62.00, 20.96, '2025-11-01 05:30:06'),
(43, 8, 165.00, 55.00, 20.20, '2025-11-01 05:37:14'),
(44, 8, 166.00, 56.00, 20.32, '2025-11-02 12:35:23'),
(45, 8, 170.00, 55.00, 19.03, '2025-11-03 14:23:10'),
(46, 8, 168.00, 53.00, 18.78, '2025-11-03 14:23:39'),
(47, 8, 167.00, 55.00, 19.72, '2025-11-03 15:43:08'),
(48, 8, 167.00, 55.00, 19.72, '2025-11-03 15:43:12'),
(49, 8, 180.00, 75.00, 23.15, '2025-11-04 18:22:43'),
(50, 8, 181.00, 78.00, 23.81, '2025-11-06 01:43:21'),
(51, 8, 182.00, 79.00, 23.85, '2025-11-06 01:48:42'),
(52, 8, 185.00, 78.00, 22.79, '2025-11-10 15:01:11'),
(53, 8, 167.00, 55.00, 19.72, '2025-11-11 04:49:02'),
(54, 8, 170.00, 55.00, 19.03, '2025-11-11 06:17:00'),
(55, 8, 166.00, 80.00, 29.03, '2025-11-11 07:32:17'),
(56, 8, 180.00, 65.00, 20.06, '2025-11-11 08:41:27'),
(57, 8, 178.00, 56.00, 17.67, '2025-11-11 08:50:07'),
(58, 8, 168.00, 50.00, 17.72, '2025-11-11 08:50:42'),
(59, 8, 168.00, 58.00, 20.55, '2025-11-11 08:50:48'),
(60, 8, 168.00, 56.00, 19.84, '2025-11-11 08:51:19');

-- --------------------------------------------------------

--
-- Table structure for table `course_programs`
--

CREATE TABLE `course_programs` (
  `course_id` int(11) NOT NULL,
  `course_name` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `course_programs`
--

INSERT INTO `course_programs` (`course_id`, `course_name`) VALUES
(1, 'Bachelor of Science in Accountancy'),
(2, 'Bachelor of Science in Management Accounting'),
(3, 'Bachelor of Science in Entrepreneurship'),
(4, 'Bachelor of Science in Tourism Management'),
(5, 'Bachelor of Arts in Communication'),
(6, 'Bachelor of Multimedia Arts'),
(7, 'Bachelor of Science in Computer Science'),
(8, 'Bachelor of Science in Information Systems'),
(9, 'Bachelor of Science in Entertainment & Multimedia Computing'),
(10, 'Bachelor of Science in Architecture'),
(11, 'Bachelor of Science in Chemical Engineering'),
(12, 'Bachelor of Science in Civil Engineering'),
(13, 'Bachelor of Science in Computer Engineering'),
(14, 'Bachelor of Science in Electrical Engineering'),
(15, 'Bachelor of Science in Electronics Engineering'),
(16, 'Bachelor of Science in Industrial Engineering'),
(17, 'Bachelor of Science in Mechanical Engineering'),
(18, 'Bachelor of Science in Biology'),
(19, 'Bachelor of Science in Pharmacy'),
(20, 'Bachelor of Science in Physical Therapy'),
(21, 'Bachelor of Science in Psychology');

-- --------------------------------------------------------

--
-- Table structure for table `enrollment_statuses`
--

CREATE TABLE `enrollment_statuses` (
  `status_id` int(11) NOT NULL,
  `status_name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `enrollment_statuses`
--

INSERT INTO `enrollment_statuses` (`status_id`, `status_name`) VALUES
(1, 'Enrolled'),
(2, 'Dropped'),
(3, 'Graduated');

-- --------------------------------------------------------

--
-- Table structure for table `medicineinventory`
--

CREATE TABLE `medicineinventory` (
  `inventory_id` int(11) NOT NULL,
  `medicine_id` int(11) DEFAULT NULL,
  `medicine_name` varchar(100) NOT NULL,
  `amount` int(11) NOT NULL,
  `added_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `added_by` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `medicineinventory`
--

INSERT INTO `medicineinventory` (`inventory_id`, `medicine_id`, `medicine_name`, `amount`, `added_at`, `added_by`) VALUES
(23, 1, 'Biogesic', 101, '2025-10-06 23:49:23', 1),
(24, 2, 'Amoxicillin', 42, '2025-10-06 23:49:23', 1),
(25, 3, 'Ventolin', 26, '2025-10-06 23:49:23', 1),
(26, 4, 'Cetirizine', 84, '2025-10-06 23:49:23', 1),
(27, 5, 'Kremil-S', 100, '2025-10-06 23:49:23', 1),
(28, 6, 'Neozep', 100, '2025-10-06 23:49:23', 1),
(29, 7, 'Bioflu', 32, '2025-10-06 23:49:23', 1),
(30, 8, 'Medicol Advance', 24, '2025-10-06 23:49:23', 1),
(31, 9, 'Ascof Lagundi', 100, '2025-10-06 23:49:23', 1),
(32, 10, 'Tuseran Forte', 21, '2025-10-06 23:49:23', 1),
(33, 11, 'Lagundi', 20, '2025-10-28 07:00:43', 1),
(34, 12, 'Rubitosin', 20, '2025-10-28 07:04:52', 1),
(35, 13, 'sdff', 31, '2025-10-28 07:05:29', 1),
(36, 14, 'dasd', 22, '2025-10-28 07:14:05', 1),
(37, 15, 'test', 19, '2025-10-31 11:03:26', 1);

-- --------------------------------------------------------

--
-- Table structure for table `medicinerequests`
--

CREATE TABLE `medicinerequests` (
  `request_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `medicine_name` varchar(100) NOT NULL,
  `reason` text DEFAULT NULL,
  `quantity` int(11) NOT NULL,
  `status` enum('Pending','Approved','Rejected') DEFAULT 'Pending',
  `requested_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `approved_date` date DEFAULT NULL,
  `reject_reason` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `medicinerequests`
--

INSERT INTO `medicinerequests` (`request_id`, `user_id`, `medicine_name`, `reason`, `quantity`, `status`, `requested_at`, `approved_date`, `reject_reason`) VALUES
(1, 8, 'Cetirizine', 'sakit akung tiil', 2, 'Rejected', '2025-10-08 03:33:55', '2025-11-03', 'dasda'),
(2, 8, 'Biogesic', '', 5, 'Approved', '2025-10-10 08:55:10', '2025-11-08', 'fgfgf'),
(3, 8, 'Biogesic', 'fdf', 5, 'Approved', '2025-10-10 08:55:12', '2025-10-25', 'no stock'),
(4, 8, 'Biogesic', 'fds', 5, 'Approved', '2025-10-10 08:55:19', '2025-10-25', 'sdad'),
(5, 8, 'Amoxicillin', 'dsdad', 4, 'Approved', '2025-10-10 08:55:25', '2025-10-28', NULL),
(6, 8, 'Cetirizine', 'dasd', 43, 'Rejected', '2025-10-10 08:55:32', '2025-10-31', 'dasda'),
(7, 8, 'Biogesic', 'dasda', 5, 'Approved', '2025-10-15 08:35:56', '2025-10-24', NULL),
(8, 8, 'Biogesic', 'ddadad', 3, 'Pending', '2025-10-15 08:40:12', NULL, NULL),
(9, 8, 'Biogesic', 'ddasd', 3, 'Rejected', '2025-10-21 14:48:01', '2025-10-21', 'Bawal'),
(10, 8, 'Biogesic', 'sdad', 100, 'Approved', '2025-10-23 07:10:46', '2025-10-23', NULL),
(11, 8, 'Biogesic', 'dsdada', 5, 'Pending', '2025-10-23 07:10:59', NULL, NULL),
(12, 8, 'Biogesic', 'dsda', 10, 'Pending', '2025-10-23 07:17:12', NULL, NULL),
(13, 8, 'Biogesic', 'ddad', 5, 'Pending', '2025-10-27 15:31:44', NULL, NULL),
(14, 8, 'Biogesic', 'dsada', 10, 'Pending', '2025-10-31 09:37:03', NULL, NULL),
(15, 8, 'Biogesic', 'dsada', 10, 'Pending', '2025-10-31 09:37:04', NULL, NULL),
(16, 8, 'Amoxicillin', 'dsaa', 4, 'Approved', '2025-10-31 09:37:22', '2025-10-31', NULL),
(17, 8, 'Biogesic', 'dasd', 5, 'Approved', '2025-10-31 09:39:58', '2025-10-31', NULL),
(18, 8, 'Biogesic', 'afafds', 10, 'Approved', '2025-11-01 04:24:11', '2025-11-01', NULL),
(19, 8, 'Biogesic', '', 4, 'Approved', '2025-11-01 04:24:24', '2025-11-01', NULL),
(20, 8, 'Biogesic', '', -5, 'Approved', '2025-11-01 05:53:17', '2025-11-01', 'sfsdf'),
(21, 8, 'Biogesic', 'Ubo', 5, 'Pending', '2025-11-01 06:01:02', NULL, NULL),
(22, 8, 'Biogesic', '', 0, 'Pending', '2025-11-01 06:19:35', NULL, NULL),
(23, 8, 'Biogesic', 'qwwr', 5, 'Pending', '2025-11-03 14:06:29', NULL, NULL),
(24, 8, 'Biogesic', 'dsda', 10, 'Pending', '2025-11-03 14:23:19', NULL, NULL),
(25, 8, 'Biogesic', 'Fever and Headache', 5, 'Pending', '2025-11-03 15:43:40', NULL, NULL),
(26, 8, 'Biogesic', 'dasdas', 5, 'Pending', '2025-11-04 19:11:31', NULL, NULL),
(27, 8, 'Biogesic', 'asda', 4, 'Pending', '2025-11-04 19:13:03', NULL, NULL),
(28, 8, 'Biogesic', 'Fever and Headache', 2, 'Pending', '2025-11-06 01:49:13', NULL, NULL),
(29, 8, 'Biogesic', 'Headache', 2, 'Pending', '2025-11-10 15:01:32', NULL, NULL),
(30, 8, 'Biogesic', 'Fever', 5, 'Pending', '2025-11-11 04:50:01', NULL, NULL),
(31, 8, 'Biogesic', 'dasdad', 1, 'Pending', '2025-11-11 05:32:31', NULL, NULL),
(32, 8, 'Biogesic', 'dadsa', 5, 'Pending', '2025-11-11 05:34:21', NULL, NULL),
(33, 8, 'Biogesic', 'adsada', 5, 'Pending', '2025-11-11 06:17:11', NULL, NULL);

-- --------------------------------------------------------

--
-- Table structure for table `medicine_info`
--

CREATE TABLE `medicine_info` (
  `medicine_id` int(11) NOT NULL,
  `medicine_name` varchar(100) NOT NULL,
  `milligrams` varchar(20) DEFAULT NULL,
  `generic_name` varchar(100) DEFAULT NULL,
  `description` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `medicine_info`
--

INSERT INTO `medicine_info` (`medicine_id`, `medicine_name`, `milligrams`, `generic_name`, `description`) VALUES
(1, 'Biogesic', '250 mg', 'Paracetamol', 'Used to relieve pain and reduce fever.'),
(2, 'Amoxicillin', '550 mg', 'Amoxicillin', 'Antibiotic used to treat bacterial infections.'),
(3, 'Ventolin', '2 mg', 'Salbutamol', 'Relieves asthma and breathing difficulties.'),
(4, 'Cetirizine', '10 mg', 'Cetirizine Hydrochloride', 'Used to relieve allergy symptoms such as runny nose and sneezing.'),
(5, 'Kremil-S', '500 mg', 'Aluminum Hydroxide + Magnesium Hydroxide + Simeticone', 'Used to relieve hyperacidity and heartburn.'),
(6, 'Neozep', '500 mg', 'Phenylephrine HCl + Chlorphenamine Maleate + Paracetamol', 'For relief of colds, fever, and headache.'),
(7, 'Bioflu', '500 mg', 'Phenylephrine HCl + Chlorphenamine Maleate + Paracetamol', 'Used to relieve symptoms of flu such as headache, fever, and nasal congestion.'),
(8, 'Medicol Advance', '400 mg', 'Ibuprofen', 'Used for pain relief and inflammation reduction.'),
(9, 'Ascof Lagundi', '600 mg', 'Vitex Negundo', 'Herbal medicine for cough relief.'),
(10, 'Tuseran Forte', '500 mg', 'Phenylephrine HCl + Dextromethorphan HBr + Paracetamol', 'Used to relieve cough, cold, and flu symptoms.'),
(11, 'Lagundi', '250', 'Ambot', 'Para sa Ubo'),
(12, 'Rubitosin', '500', 'Ambot', 'ubo'),
(13, 'sdff', '230', 'fdsfsfd', 'sdfs'),
(14, 'dasd', '323', 'dad', 'adasd'),
(15, 'test', '150', 'test', 'sadada');

-- --------------------------------------------------------

--
-- Table structure for table `student_activity_log`
--

CREATE TABLE `student_activity_log` (
  `activity_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `activity_type` varchar(50) NOT NULL,
  `activity_desc` text NOT NULL,
  `activity_date` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `student_activity_log`
--

INSERT INTO `student_activity_log` (`activity_id`, `user_id`, `activity_type`, `activity_desc`, `activity_date`) VALUES
(1, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-09 23:25:19'),
(2, 8, 'Appointment', 'Request Medicine for ', '2025-10-10 16:55:10'),
(3, 8, 'Appointment', 'Request Medicine for fdf', '2025-10-10 16:55:13'),
(4, 8, 'Appointment', 'Request Medicine for fds', '2025-10-10 16:55:20'),
(5, 8, 'Appointment', 'Request Medicine for dsdad', '2025-10-10 16:55:25'),
(6, 8, 'Appointment', 'Request Medicine for dasd', '2025-10-10 16:55:33'),
(7, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-10 20:35:36'),
(8, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-10 20:35:44'),
(9, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-10 20:35:53'),
(10, 8, 'Vitals Check', 'BMI Check Up', '2025-10-11 00:08:24'),
(11, 8, 'Vitals Check', 'BMI Check Up', '2025-10-11 00:08:25'),
(12, 8, 'Vitals Check', 'BMI Check Up', '2025-10-11 00:11:29'),
(13, 8, 'Vitals Check', 'BMI Check Up', '2025-10-14 21:25:04'),
(14, 8, 'Vitals Check', 'BMI Check Up', '2025-10-14 21:25:46'),
(15, 8, 'Vitals Check', 'BMI Check Up', '2025-10-14 21:25:50'),
(16, 8, 'Vitals Check', 'BMI Check Up', '2025-10-14 21:34:01'),
(17, 8, 'Vitals Check', 'BMI Check Up', '2025-10-14 21:42:19'),
(18, 8, 'Vitals Check', 'BMI Check Up', '2025-10-14 22:29:13'),
(19, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 11:05:53'),
(20, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 11:09:06'),
(21, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-15 12:35:47'),
(22, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-15 14:04:20'),
(23, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-15 14:05:09'),
(24, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-15 14:12:57'),
(25, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-15 14:20:31'),
(26, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-15 14:24:20'),
(27, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-15 14:32:24'),
(28, 8, 'Appointment', 'Update appointment for Injury', '2025-10-15 15:06:28'),
(29, 8, 'Appointment', 'Update appointment for Injury', '2025-10-15 15:07:17'),
(30, 8, 'Appointment', 'Update appointment for Injury', '2025-10-15 15:33:27'),
(31, 8, 'Appointment', 'Update appointment for Injury', '2025-10-15 15:34:02'),
(32, 8, 'Appointment', 'Update appointment for Check-up', '2025-10-15 15:34:29'),
(33, 8, 'Appointment', 'Update appointment for Injury', '2025-10-15 15:34:43'),
(34, 8, 'Appointment', 'Request Medicine for dasda', '2025-10-15 16:35:57'),
(35, 8, 'Appointment', 'Request Medicine for ddadad', '2025-10-15 16:40:13'),
(36, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 18:06:47'),
(37, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 18:07:01'),
(38, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 18:07:18'),
(39, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 18:07:24'),
(40, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 18:07:31'),
(41, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 18:07:31'),
(42, 8, 'Vitals Check', 'BMI Check Up', '2025-10-15 18:07:53'),
(43, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-15 22:37:16'),
(44, 8, 'Appointment', 'Update appointment for Injury', '2025-10-15 22:49:49'),
(45, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-16 18:50:15'),
(46, 8, 'Vitals Check', 'BMI Check Up', '2025-10-16 20:18:10'),
(47, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-18 08:40:32'),
(48, 8, 'Appointment', 'Update appointment for Injury', '2025-10-18 08:41:26'),
(49, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-18 09:36:59'),
(50, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-18 09:40:00'),
(51, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-18 09:40:10'),
(52, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-18 09:40:16'),
(53, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-18 10:09:30'),
(54, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-18 10:09:36'),
(55, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-20 15:33:05'),
(56, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-20 17:09:01'),
(57, 8, 'Vitals Check', 'BMI Check Up', '2025-10-20 18:13:11'),
(58, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-20 18:13:37'),
(59, 8, 'Appointment', 'Update appointment for Injury', '2025-10-20 18:17:21'),
(60, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-20 20:07:52'),
(61, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-20 22:53:45'),
(62, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-20 23:17:54'),
(63, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-21 00:20:18'),
(64, 8, 'Appointment', 'Cancel appointment', '2025-10-21 16:51:21'),
(65, 8, 'Appointment', 'Cancel appointment 41', '2025-10-21 16:53:50'),
(66, 8, 'Appointment', 'Update appointment ID:42', '2025-10-21 16:55:56'),
(67, 8, 'Appointment', 'Cancel appointment ID:42', '2025-10-21 16:56:03'),
(68, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-21 17:00:25'),
(69, 8, 'Vitals Check', 'BMI Check Up', '2025-10-21 17:06:45'),
(70, 8, 'Vitals Check', 'BMI Check Up', '2025-10-21 17:07:14'),
(71, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-21 17:19:52'),
(72, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-21 17:40:25'),
(73, 8, 'Vitals Check', 'BMI Check Up', '2025-10-21 22:47:30'),
(74, 8, 'Appointment', 'Request Medicine for ddasd', '2025-10-21 22:48:02'),
(75, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-22 02:30:07'),
(76, 8, 'Appointment', 'Cancel appointment ID: 44', '2025-10-22 02:30:55'),
(77, 8, 'Appointment', 'Update appointment ID: 45', '2025-10-22 12:36:22'),
(78, 8, 'Appointment', 'Cancel appointment ID: 45', '2025-10-22 12:36:32'),
(79, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-22 20:38:40'),
(80, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-23 15:05:49'),
(81, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-23 15:06:49'),
(82, 8, 'Vitals Check', 'BMI Check Up', '2025-10-23 15:10:14'),
(83, 8, 'Appointment', 'Request Medicine for sdad', '2025-10-23 15:10:47'),
(84, 8, 'Appointment', 'Request Medicine for dsdada', '2025-10-23 15:11:00'),
(85, 8, 'Appointment', 'Request Medicine for dsda', '2025-10-23 15:17:13'),
(86, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-23 16:29:49'),
(87, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 01:42:56'),
(88, 8, 'Appointment', 'Update appointment ID: 52', '2025-10-27 01:43:09'),
(89, 8, 'Appointment', 'Cancel appointment ID: 52', '2025-10-27 01:43:15'),
(90, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 01:45:42'),
(91, 8, 'Appointment', 'Update appointment ID: 53', '2025-10-27 01:45:50'),
(92, 8, 'Appointment', 'Cancel appointment ID: 53', '2025-10-27 01:45:53'),
(93, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 04:26:35'),
(94, 8, 'Appointment', 'Update appointment ID: 54', '2025-10-27 04:26:54'),
(95, 8, 'Appointment', 'Cancel appointment ID: 54', '2025-10-27 04:26:59'),
(96, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 04:36:02'),
(97, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 05:20:50'),
(98, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 05:26:13'),
(99, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-27 05:28:04'),
(100, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-27 05:28:14'),
(101, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-27 05:31:06'),
(102, 8, 'Appointment', 'Booking appointment for 59', '2025-10-27 12:03:57'),
(103, 8, 'Appointment', 'Booking appointment for 59', '2025-10-27 12:04:12'),
(104, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-27 12:07:13'),
(105, 8, 'Appointment', 'Update appointment ID:59', '2025-10-27 12:08:11'),
(106, 8, 'Appointment', 'Cancel appointment ID: 55', '2025-10-27 12:08:24'),
(107, 8, 'Appointment', 'Booking appointment for ', '2025-10-27 12:08:34'),
(108, 8, 'Appointment', 'Cancel appointment ID: 63', '2025-10-27 12:08:47'),
(109, 8, 'Appointment', 'Cancel appointment ID: 61', '2025-10-27 12:08:49'),
(110, 8, 'Appointment', 'Cancel appointment ID: 59', '2025-10-27 12:08:50'),
(111, 8, 'Appointment', 'Booking appointment for ', '2025-10-27 12:08:57'),
(112, 8, 'Appointment', 'Booking appointment for ', '2025-10-27 12:09:12'),
(113, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 12:10:22'),
(114, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 18:09:16'),
(115, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-27 18:09:46'),
(116, 8, 'Appointment', 'Update appointment ID:68', '2025-10-27 18:10:00'),
(117, 8, 'Appointment', 'Cancel appointment ID: 68', '2025-10-27 18:10:06'),
(118, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-27 18:11:19'),
(119, 8, 'Appointment', 'Cancel appointment ID: 69', '2025-10-27 18:11:26'),
(120, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-27 18:11:43'),
(121, 8, 'Appointment', 'Update appointment ID:70', '2025-10-27 18:11:57'),
(122, 8, 'Appointment', 'Cancel appointment ID: 70', '2025-10-27 18:12:02'),
(123, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-27 23:07:03'),
(124, 8, 'Appointment', 'Request Medicine for ddad', '2025-10-27 23:31:45'),
(125, 8, 'Vitals Check', 'BMI Check Up', '2025-10-28 02:16:16'),
(126, 8, 'Vitals Check', 'BMI Check Up', '2025-10-28 02:16:37'),
(127, 8, 'Vitals Check', 'BMI Check Up', '2025-10-28 02:23:30'),
(128, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-28 04:33:22'),
(129, 8, 'Vitals Check', 'BMI Check Up', '2025-10-28 04:43:41'),
(130, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-28 04:49:49'),
(131, 8, 'Vitals Check', 'BMI Check Up', '2025-10-28 04:50:39'),
(132, 8, 'Appointment', 'Update appointment ID:74', '2025-10-28 04:52:30'),
(133, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-31 17:19:55'),
(134, 8, 'Appointment', 'Booking appointment for Check-up', '2025-10-31 17:32:19'),
(135, 8, 'Appointment', 'Booking appointment for Injury', '2025-10-31 17:34:07'),
(136, 8, 'Appointment', 'Request Medicine for dsada', '2025-10-31 17:37:03'),
(137, 8, 'Appointment', 'Request Medicine for dsada', '2025-10-31 17:37:05'),
(138, 8, 'Appointment', 'Request Medicine for dsaa', '2025-10-31 17:37:22'),
(139, 8, 'Appointment', 'Request Medicine for dasd', '2025-10-31 17:39:58'),
(140, 8, 'Vitals Check', 'BMI Check Up', '2025-10-31 17:43:01'),
(141, 8, 'Appointment', 'Booking appointment for Check-up', '2025-11-01 12:23:46'),
(142, 8, 'Vitals Check', 'BMI Check Up', '2025-11-01 12:23:58'),
(143, 8, 'Appointment', 'Request Medicine for afafds', '2025-11-01 12:24:11'),
(144, 8, 'Appointment', 'Request Medicine for ', '2025-11-01 12:24:24'),
(145, 8, 'Appointment', 'Cancel appointment ID: 75', '2025-11-01 12:24:53'),
(146, 8, 'Appointment', 'Cancel appointment ID: 78', '2025-11-01 12:30:32'),
(147, 8, 'Appointment', 'Cancel appointment ID: 76', '2025-11-01 12:31:27'),
(148, 8, 'Appointment', 'Cancel appointment ID: 77', '2025-11-01 12:31:32'),
(149, 8, 'Vitals Check', 'BMI Check Up', '2025-11-01 13:30:06'),
(150, 8, 'Vitals Check', 'BMI Check Up', '2025-11-01 13:37:14'),
(151, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-01 13:39:03'),
(152, 8, 'Appointment', 'Request Medicine for ', '2025-11-01 13:53:17'),
(153, 8, 'Appointment', 'Request Medicine for Ubo', '2025-11-01 14:01:02'),
(154, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-01 14:07:18'),
(155, 8, 'Appointment', 'Request Medicine for ', '2025-11-01 14:19:35'),
(156, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-02 12:41:20'),
(157, 8, 'Appointment', 'Booking appointment for Check-up', '2025-11-02 20:34:49'),
(158, 8, 'Vitals Check', 'BMI Check Up', '2025-11-02 20:35:23'),
(159, 8, 'Appointment', 'Request Medicine for qwwr', '2025-11-03 22:06:29'),
(160, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-03 22:22:58'),
(161, 8, 'Vitals Check', 'BMI Check Up', '2025-11-03 22:23:10'),
(162, 8, 'Appointment', 'Request Medicine for dsda', '2025-11-03 22:23:19'),
(163, 8, 'Vitals Check', 'BMI Check Up', '2025-11-03 22:23:40'),
(164, 8, 'Appointment', 'Update appointment ID:83', '2025-11-03 22:23:50'),
(165, 8, 'Appointment', 'Cancel appointment ID: 83', '2025-11-03 22:24:00'),
(166, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-03 23:40:53'),
(167, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-03 23:41:12'),
(168, 8, 'Appointment', 'Update appointment ID:84', '2025-11-03 23:42:17'),
(169, 8, 'Appointment', 'Cancel appointment ID: 84', '2025-11-03 23:42:36'),
(170, 8, 'Vitals Check', 'BMI Check Up', '2025-11-03 23:43:08'),
(171, 8, 'Vitals Check', 'BMI Check Up', '2025-11-03 23:43:12'),
(172, 8, 'Appointment', 'Request Medicine for Fever and Headache', '2025-11-03 23:43:40'),
(173, 8, 'Appointment', 'Booking appointment for Check-up', '2025-11-05 00:58:39'),
(174, 8, 'Vitals Check', 'BMI Check Up', '2025-11-05 02:22:43'),
(175, 8, 'Appointment', 'Request Medicine for dasdas', '2025-11-05 03:11:31'),
(176, 8, 'Medicine Request', 'Request Medicine for asda', '2025-11-05 03:13:03'),
(177, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-06 09:03:24'),
(178, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-06 09:05:31'),
(179, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-06 09:42:53'),
(180, 8, 'Vitals Check', 'BMI Check Up', '2025-11-06 09:43:21'),
(181, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-06 09:48:14'),
(182, 8, 'Vitals Check', 'BMI Check Up', '2025-11-06 09:48:42'),
(183, 8, 'Medicine Request', 'Request Medicine for Fever and Headache', '2025-11-06 09:49:13'),
(184, 8, 'Appointment', 'Update appointment ID:89', '2025-11-06 09:49:51'),
(185, 8, 'Appointment', 'Cancel appointment ID: 87', '2025-11-06 09:50:04'),
(186, 8, 'Appointment', 'Booking appointment for Injury', '2025-11-10 23:00:55'),
(187, 8, 'Vitals Check', 'BMI Check Up', '2025-11-10 23:01:11'),
(188, 8, 'Medicine Request', 'Request Medicine for Headache', '2025-11-10 23:01:32'),
(189, 8, 'Appointment', 'Update appointment ID:91', '2025-11-10 23:01:52'),
(190, 8, 'Appointment', 'Cancel appointment ID: 91', '2025-11-10 23:02:03'),
(191, 8, 'Appointment', 'Booking appointment for Check-up', '2025-11-11 12:47:38'),
(192, 8, 'Appointment', 'Update appointment ID:92', '2025-11-11 12:48:21'),
(193, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 12:49:02'),
(194, 8, 'Medicine Request', 'Request Medicine for Fever', '2025-11-11 12:50:01'),
(195, 8, 'Medicine Request', 'Request Medicine for dasdad', '2025-11-11 13:32:31'),
(196, 8, 'Medicine Request', 'Request Medicine for dadsa', '2025-11-11 13:34:21'),
(197, 8, 'Appointment', 'Booking appointment for Check-up', '2025-11-11 14:16:31'),
(198, 8, 'Appointment', 'Update appointment ID:92', '2025-11-11 14:16:46'),
(199, 8, 'Appointment', 'Cancel appointment ID: 93', '2025-11-11 14:16:53'),
(200, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 14:17:00'),
(201, 8, 'Medicine Request', 'Request Medicine for adsada', '2025-11-11 14:17:11'),
(202, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 15:32:17'),
(203, 8, 'Appointment', 'Booking appointment for Check-up', '2025-11-11 16:36:44'),
(204, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 16:41:27'),
(205, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 16:50:07'),
(206, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 16:50:42'),
(207, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 16:50:48'),
(208, 8, 'Vitals Check', 'BMI Check Up', '2025-11-11 16:51:19');

-- --------------------------------------------------------

--
-- Table structure for table `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `username` varchar(50) DEFAULT NULL,
  `role` enum('Admin','Student') NOT NULL,
  `first_name` varchar(50) NOT NULL,
  `last_name` varchar(50) NOT NULL,
  `student_id` varchar(20) DEFAULT NULL,
  `date_of_birth` date DEFAULT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(100) NOT NULL,
  `phone_number` varchar(20) DEFAULT NULL,
  `address` varchar(255) DEFAULT NULL,
  `course_program` varchar(100) DEFAULT NULL,
  `year_level` varchar(11) DEFAULT NULL,
  `blood_type` varchar(3) DEFAULT NULL,
  `enrollment_status` enum('Enrolled','Not Enrolled','Graduated') DEFAULT 'Enrolled',
  `created_at` timestamp NOT NULL DEFAULT current_timestamp(),
  `emergency_contact_name` varchar(100) DEFAULT NULL,
  `emergency_contact_phone` varchar(20) DEFAULT NULL,
  `known_allergies` text DEFAULT NULL,
  `medical_conditions` text DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `users`
--

INSERT INTO `users` (`user_id`, `username`, `role`, `first_name`, `last_name`, `student_id`, `date_of_birth`, `email`, `password`, `phone_number`, `address`, `course_program`, `year_level`, `blood_type`, `enrollment_status`, `created_at`, `emergency_contact_name`, `emergency_contact_phone`, `known_allergies`, `medical_conditions`) VALUES
(1, 'admin', 'Admin', 'System', 'Administrator', '2022323', '1990-01-01', 'admin@example.local', 'Admin@123', '09171234567', 'Main Admin Office', 'Bachelor of Multimedia Arts', '3rd Year', 'B-', 'Enrolled', '2025-10-02 13:13:33', 'dsd', '231313', 'dsd', 'dsd'),
(8, 'jdelacruz', 'Student', 'Juan', 'Dela Cruz', '2025005', '2006-03-15', 'juan.delacruz@example.com', 'password123', '09171234565', 'Davao City', 'Bachelor of Science in Entrepreneurship', '2nd Year', 'AB+', 'Enrolled', '2025-10-05 02:25:11', 'sdad', '3123', 'eqeq', '32313'),
(14, 'dperez', 'Student', 'Diego', 'Perez', '2025007', '2004-09-07', 'diego.perez@example.com', 'password123', '09201234563', 'Davao Oriental', 'Bachelor of Science in Management Accounting', '2nd Year', 'B-', 'Enrolled', '2025-10-05 02:25:11', '', '', '', ''),
(18, 'csSantiago', 'Admin', 'Conrad', 'Santiago', '323323', '2025-10-06', 'dadadsda', 'dadad', '32131', 'dasdad', 'BS Computer Science', '2', 'B+', 'Enrolled', '2025-10-05 20:30:39', NULL, NULL, NULL, NULL),
(19, 'fhfhfgh', 'Admin', 'gjgjgj', 'opojpoi', 't56858', '2025-10-06', 'jgkhgk', 'fhgfh', '98870987', 'jhlkjhlk', 'BS Computer Science', '2', 'AB+', 'Enrolled', '2025-10-05 20:34:26', NULL, NULL, NULL, NULL),
(20, 'lester', 'Admin', 'lester', 'arigo', '2023121062', '2006-01-23', 'jlArigo@mcm.edu.ph', 'lester123', '09560498124', 'deca mintal', 'BS Computer Science', '2', 'O-', 'Enrolled', '2025-10-08 01:44:53', NULL, NULL, NULL, NULL),
(21, 'test', 'Admin', 'test', 'test', '123', '2025-10-02', 'test', 'test', '123', 'test', 'BS Computer Science', '1', 'B-', 'Enrolled', '2025-10-08 01:49:17', NULL, NULL, NULL, NULL),
(23, 'jcSantiago', 'Student', 'James', 'Santiago', '12345', '2021-12-24', 'James@gmail.com', 'James123', ')9702514629', 'Bugac Maa', 'Bachelor of Science in Information Technology', '3rd Year', 'B-', 'Enrolled', '2025-10-14 01:29:56', NULL, NULL, NULL, NULL),
(25, 'jmSantiago', 'Student', 'John Marco', 'Santiago', '2022120096', '2006-09-28', 'jmSantiago@mcm.edu.ph', '123456789', '09702514629', 'Purok 13-A Bugac Ma-a Davao City', 'Bachelor of Science in Accountancy', '4th Year', 'O+', 'Enrolled', '2025-10-21 17:16:42', 'Marilen Santiago', '09702514629', 'Peanut', 'Anemic'),
(30, 'fsfs', 'Admin', 'dsdas', 'sadada', '454354', '2025-10-11', 'fsdffsdfs', 'fsdfds', 'fsdfs', 'fsfsfs', 'Bachelor of Science in Information Technology', '3rd Year', 'B+', 'Enrolled', '2025-10-21 17:38:12', 'dsfsfs', '3123', 'fsdfsf', 'fsfsfs'),
(33, 'sdadas', 'Admin', 'add', 'adasda', '23132', '2025-10-03', 'dada', 'ddasd', '3123', 'dada', 'Bachelor of Science in Entrepreneurship', '2nd Year', 'AB+', 'Enrolled', '2025-10-24 20:23:14', 'dada', 'dasd', 'dada', 'sdada'),
(35, 'dsda', 'Admin', 'dad', 'dasd', '31232', '2025-10-10', 'asdad', 'dasd', 'dasda', 'dasda', 'Bachelor of Science in Entrepreneurship', '2nd Year', 'B-', 'Enrolled', '2025-10-27 06:08:26', 'dad', 'adad', 'dada', 'dad'),
(37, 'A', 'Student', 'A', 'A', '1', '2025-11-06', 'A', 'A', '1', 'A', 'Bachelor of Science in Management Accounting', '1st Year', 'A-', '', '2025-11-11 04:40:05', 'A', '1', 'A', 'A');

-- --------------------------------------------------------

--
-- Table structure for table `year_levels`
--

CREATE TABLE `year_levels` (
  `year_level_id` int(11) NOT NULL,
  `level_name` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Dumping data for table `year_levels`
--

INSERT INTO `year_levels` (`year_level_id`, `level_name`) VALUES
(1, '1st Year'),
(2, '2nd Year'),
(3, '3rd Year'),
(4, '4th Year');

--
-- Indexes for dumped tables
--

--
-- Indexes for table `admin_activity_log`
--
ALTER TABLE `admin_activity_log`
  ADD PRIMARY KEY (`activity_id`);

--
-- Indexes for table `appointments`
--
ALTER TABLE `appointments`
  ADD PRIMARY KEY (`appointment_id`),
  ADD UNIQUE KEY `unique_appointment_time` (`appointment_date`,`appointment_time`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `blood_types`
--
ALTER TABLE `blood_types`
  ADD PRIMARY KEY (`blood_type_id`);

--
-- Indexes for table `checkups`
--
ALTER TABLE `checkups`
  ADD PRIMARY KEY (`checkup_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `course_programs`
--
ALTER TABLE `course_programs`
  ADD PRIMARY KEY (`course_id`);

--
-- Indexes for table `enrollment_statuses`
--
ALTER TABLE `enrollment_statuses`
  ADD PRIMARY KEY (`status_id`);

--
-- Indexes for table `medicineinventory`
--
ALTER TABLE `medicineinventory`
  ADD PRIMARY KEY (`inventory_id`),
  ADD KEY `added_by` (`added_by`);

--
-- Indexes for table `medicinerequests`
--
ALTER TABLE `medicinerequests`
  ADD PRIMARY KEY (`request_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `medicine_info`
--
ALTER TABLE `medicine_info`
  ADD PRIMARY KEY (`medicine_id`);

--
-- Indexes for table `student_activity_log`
--
ALTER TABLE `student_activity_log`
  ADD PRIMARY KEY (`activity_id`),
  ADD KEY `user_id` (`user_id`);

--
-- Indexes for table `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD UNIQUE KEY `student_id` (`student_id`),
  ADD UNIQUE KEY `username` (`username`);

--
-- Indexes for table `year_levels`
--
ALTER TABLE `year_levels`
  ADD PRIMARY KEY (`year_level_id`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `admin_activity_log`
--
ALTER TABLE `admin_activity_log`
  MODIFY `activity_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=197;

--
-- AUTO_INCREMENT for table `appointments`
--
ALTER TABLE `appointments`
  MODIFY `appointment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=95;

--
-- AUTO_INCREMENT for table `blood_types`
--
ALTER TABLE `blood_types`
  MODIFY `blood_type_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT for table `checkups`
--
ALTER TABLE `checkups`
  MODIFY `checkup_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=61;

--
-- AUTO_INCREMENT for table `course_programs`
--
ALTER TABLE `course_programs`
  MODIFY `course_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT for table `enrollment_statuses`
--
ALTER TABLE `enrollment_statuses`
  MODIFY `status_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT for table `medicineinventory`
--
ALTER TABLE `medicineinventory`
  MODIFY `inventory_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `medicinerequests`
--
ALTER TABLE `medicinerequests`
  MODIFY `request_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=34;

--
-- AUTO_INCREMENT for table `medicine_info`
--
ALTER TABLE `medicine_info`
  MODIFY `medicine_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=16;

--
-- AUTO_INCREMENT for table `student_activity_log`
--
ALTER TABLE `student_activity_log`
  MODIFY `activity_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=209;

--
-- AUTO_INCREMENT for table `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=38;

--
-- AUTO_INCREMENT for table `year_levels`
--
ALTER TABLE `year_levels`
  MODIFY `year_level_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- Constraints for dumped tables
--

--
-- Constraints for table `appointments`
--
ALTER TABLE `appointments`
  ADD CONSTRAINT `appointments_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `checkups`
--
ALTER TABLE `checkups`
  ADD CONSTRAINT `checkups_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `medicineinventory`
--
ALTER TABLE `medicineinventory`
  ADD CONSTRAINT `medicineinventory_ibfk_1` FOREIGN KEY (`added_by`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `medicinerequests`
--
ALTER TABLE `medicinerequests`
  ADD CONSTRAINT `medicinerequests_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Constraints for table `student_activity_log`
--
ALTER TABLE `student_activity_log`
  ADD CONSTRAINT `student_activity_log_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE ON UPDATE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
